using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Common;
using ZXing.Multi;
using ZXing.Rendering;
using ZXing.Windows.Compatibility;
using MessagingToolkit.Barcode;

namespace KasMin_Kasir_Mini_Market
{
    public partial class frmTransaksi : Form
    {
        public string CurrentUserId { get; set; } // Set saat form ini dipanggil
        public string NamaKasir { get; set; } // Set saat form ini dipanggil

        public frmTransaksi()
        {
            InitializeComponent();
        }

        private void frmTransaksi_Load(object sender, EventArgs e)
        {
            NamaKategori();
            CekTransaksiAktif();
            txtNamaKasir.Text = NamaKasir;
            txtTransaksiId.Enabled = false;
            txtNamaKasir.Enabled = false;
            cmbNamaKategori.SelectedIndex = -1;
            cmbNamaProduk.SelectedIndex = -1;
           
            cmbNamaKategori.SelectedIndexChanged += cmbNamaKategori_SelectedIndexChanged;
            cmbNamaProduk.SelectedIndexChanged += cmbNamaProduk_SelectedIndexChanged;
             txtBarcode.Text = "";




        }
        private void CekTransaksiAktif()
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();

                // 1. Cek apakah sudah ada transaksi yang belum selesai
                string cekQuery = @"SELECT transaksi_id 
                                FROM tb_transaksi 
                                WHERE user_id = @userId AND metode_pembayaran IS NULL 
                                ORDER BY tanggal DESC 
                                LIMIT 1";

                MySqlCommand cekCmd = new MySqlCommand(cekQuery, conn);
                cekCmd.Parameters.AddWithValue("@userId", CurrentUserId);
                var existingId = cekCmd.ExecuteScalar();

                if (existingId != null)
                {
                    // Jika sudah ada transaksi aktif, gunakan ID tersebut
                    txtTransaksiId.Text = existingId.ToString();
                }
                else
                {
                    // 2. Tidak ada transaksi aktif → buat baru
                    string newId = GenerateTransaksiId(conn);
                    string insertQuery = @"INSERT INTO tb_transaksi (transaksi_id, user_id, tanggal) 
                                       VALUES (@id, @userId, CURRENT_TIMESTAMP)";

                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@id", newId);
                    insertCmd.Parameters.AddWithValue("@userId", CurrentUserId);
                    insertCmd.ExecuteNonQuery();

                    txtTransaksiId.Text = newId;
                }
            }
        }

        private void NamaKategori()
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                string query = "SELECT kategori_id, nama_kategori FROM tb_kategori";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbNamaKategori.DisplayMember = "nama_kategori";
                cmbNamaKategori.ValueMember = "kategori_id";
                cmbNamaKategori.DataSource = dt;
            }
        }




        private string GenerateTransaksiId(MySqlConnection conn)
        {
            // Ambil ID transaksi terakhir yang sudah selesai
            string query = "SELECT MAX(transaksi_id) FROM tb_transaksi WHERE metode_pembayaran IS NOT NULL";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var result = cmd.ExecuteScalar()?.ToString();

            if (!string.IsNullOrEmpty(result) && result.Length >= 7)
            {
                int nomor = int.Parse(result.Substring(3)) + 1;
                return "TRN" + nomor.ToString("D4");
            }
            else
            {
                return "TRN0001";
            }
        }

        private void cmbNamaKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamaKategori.SelectedValue != null)
            {
                string selectedKategoriId = cmbNamaKategori.SelectedValue.ToString();
                LoadProdukBerdasarkanKategori(selectedKategoriId);
                cmbNamaProduk.Enabled = true;
            }
            else
            {
                cmbNamaProduk.Enabled = false;
            }
        }

        private void LoadProdukBerdasarkanKategori(string kategoriId)
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                string query = "SELECT produk_id, nama_produk, barcode FROM tb_produk WHERE kategori_id = @kategoriId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kategoriId", kategoriId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbNamaProduk.DisplayMember = "nama_produk";
                cmbNamaProduk.ValueMember = "produk_id";
                cmbNamaProduk.DataSource = dt;
            }
        }

        private void cmbNamaProduk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamaProduk.SelectedItem != null)
            {
                DataRowView selectedRow = cmbNamaProduk.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    txtBarcode.Text = selectedRow["barcode"].ToString();
                }
            }
        }
    }

}
