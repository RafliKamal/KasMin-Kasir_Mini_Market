﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;


namespace KasMin_Kasir_Mini_Market
{

    public partial class frmBayar : Form
    {
        private bool pembayaranValid = false;
        private PrintDocument printDocument = new PrintDocument();
        private string strukToPrint = "";
        public Action PerbaruiTransaksiCallback; // untuk meng-update frmTransaksi

        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        public string TransaksiId { get; set; } // Tambahkan ini
        public string Total { get; set; } // Tambahkan ini

        public string NamaKasir { get; set; } // Tambahkan ini

        public string tanggal { get; set; } // Tambahkan ini

        public frmBayar()
        {
            InitializeComponent();
            

        }

        private void frmBayar_Load(object sender, EventArgs e)
        {
            labelTotal.Text = "Rp " + Total;
            txtUangMasuk.Text = "0";
            txtKembalian.Text = "0";
            cmbMetode.Items.Add("Tunai");
            cmbMetode.Items.Add("QRIS");

            cmbMetode.Focus();
            btnLunas.Enabled = false;

            printDocument.PrintPage += PrintDocument_PrintPage;
            printPreviewDialog.ClientSize = new Size(400, 600);
            printPreviewDialog.UseAntiAlias = true;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 10);
            float lineHeight = font.GetHeight(e.Graphics) + 2;
            float x = 10;
            float y = 10;

            using (StringReader reader = new StringReader(strukToPrint))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    e.Graphics.DrawString(line, font, Brushes.Black, x, y);
                    y += lineHeight;
                }
            }
        }


        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close(); // Menutup frmBayar dan kembali ke frmTransaksi
        }

        private void btnLunas_Click(object sender, EventArgs e)
        {
            if (!pembayaranValid)
            {
                MessageBox.Show("Silakan periksa kembalian terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int uangMasuk = int.Parse(txtUangMasuk.Text);
            int total = int.Parse(Total);
            int kembalian = uangMasuk - total;

            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();

                string query = @"
            UPDATE tb_transaksi 
            SET  
                tanggal = @tanggal,
                uang_masuk = @masuk,
                kembalian = @kembali,
                grand_total = @total,
                metode_pembayaran = @metode 
            WHERE transaksi_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", TransaksiId);
                cmd.Parameters.AddWithValue("@tanggal", tanggal);
                cmd.Parameters.AddWithValue("@masuk", uangMasuk);
                cmd.Parameters.AddWithValue("@kembali", kembalian);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@metode", cmbMetode.Text);
                cmd.ExecuteNonQuery();
            }

            txtKembalian.Text = kembalian.ToString();

            BuatStruk();

            // Tampilkan preview struk (bukan mencetak langsung)
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();

            // Memanggil callback untuk memperbarui tra
            // nsaksi
            PerbaruiTransaksiCallback?.Invoke();
            if (this.Owner != null && this.Owner is frmTransaksi parentForm)
            {
                parentForm.clearField(); // sekarang cekTransaksiAktif hanya dipanggil di sini
            }

            this.Close();
        }



        private void BuatStruk()
        {
            StringBuilder struk = new StringBuilder();
            struk.AppendLine("======= STRUK PEMBAYARAN =======");
            struk.AppendLine($"Tanggal      : {DateTime.Parse(tanggal):dd-MM-yyyy}");
            struk.AppendLine($"Transaksi ID : {TransaksiId}");
            struk.AppendLine($"Kasir        : {NamaKasir}");
            struk.AppendLine("--------------------------------");

            struk.AppendLine("Item            Qty   Subtotal");
            struk.AppendLine("--------------------------------");

            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                string query = @"
        SELECT p.nama_produk, t.jumlah, t.subtotal
        FROM tb_detail_transaksi t
        JOIN tb_produk p ON t.produk_id = p.produk_id
        WHERE t.transaksi_id = @transaksi_id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@transaksi_id", TransaksiId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nama = reader["nama_produk"].ToString();
                        int jumlah = Convert.ToInt32(reader["jumlah"]);
                        int subtotal = Convert.ToInt32(reader["subtotal"]);

                        // Format kolom dengan padding: nama (15), qty (3), subtotal (right-align)
                        struk.AppendLine($"{nama.PadRight(15).Substring(0, 15)} {jumlah,3}  Rp{subtotal,8:N0}");
                    }
                }
            }

            struk.AppendLine("--------------------------------");
            struk.AppendLine($"     Total        : Rp {int.Parse(Total):N0}");
            struk.AppendLine($"     Uang Masuk   : Rp {int.Parse(txtUangMasuk.Text):N0}");
            struk.AppendLine($"     Kembalian    : Rp {int.Parse(txtKembalian.Text):N0}");
            struk.AppendLine($"     Metode Bayar : {cmbMetode.Text}");
            struk.AppendLine("================================");

            strukToPrint = struk.ToString();
        }


        private void btnCek_Click(object sender, EventArgs e)
        {
            int uangMasuk;
            if (int.TryParse(txtUangMasuk.Text, out uangMasuk))
            {
                int total = int.Parse(Total);
                int kembalian = uangMasuk - total;
                if (kembalian < 0)
                {
                    MessageBox.Show("Uang yang dibayarkan kurang dari total.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pembayaranValid = false;
                }
                else
                {
                    txtKembalian.Text = kembalian.ToString();
                    pembayaranValid = true;
                    btnLunas.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Uang masuk tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pembayaranValid = false;
            }
        }

    }
}
