using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace KasMin_Kasir_Mini_Market
{
    public partial class Laporan_Harian : Form
    {
        private DateTime selectedDate;
        private string strukToPrint = "";
        private PrintDocument printDocument = new PrintDocument();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();

        public Laporan_Harian()
        {
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ClientSize = new Size(400, 600);

            PaperSize paperSize = new PaperSize("StrukCustom", 350, 600); // width 80mm, height sesuai isi
            printDocument.DefaultPageSettings.PaperSize = paperSize;

            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ClientSize = new Size(350, 600);
            printPreviewDialog.UseAntiAlias = true;
        }

        private void LoadBulan()
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"
            SELECT DISTINCT MONTH(tanggal) AS bulan, YEAR(tanggal) AS tahun 
            FROM tb_transaksi 
            WHERE tanggal IS NOT NULL 
            ORDER BY tahun, bulan", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                cmbBulan.Items.Clear();
                cmbBulan.Items.Add("All");

                while (reader.Read())
                {
                    int bulan = reader.GetInt32("bulan");
                    int tahun = reader.GetInt32("tahun");
                    string namaBulan = new DateTime(tahun, bulan, 1).ToString("MMMM yyyy");

                    cmbBulan.Items.Add(new KeyValuePair<string, string>($"{bulan}-{tahun}", namaBulan));
                }

                cmbBulan.DisplayMember = "Value";
                cmbBulan.ValueMember = "Key";
                cmbBulan.SelectedIndex = 0;
            }
        }





        private void dataHarian_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataHarian.Rows[e.RowIndex].Cells["tanggal"].Value != DBNull.Value)
            {
                selectedDate = Convert.ToDateTime(dataHarian.Rows[e.RowIndex].Cells["tanggal"].Value);
                DisplaySeluruhTransaksi();
            }
        }


        private void Laporan_Harian_Load(object sender, EventArgs e)
        {
            LoadBulan();
            DisplayDataHarian();
            HitungTotalKeseluruhan();
        }

        private void DisplayDataHarian()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM view_pendapatan_harian WHERE tanggal IS NOT NULL", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataHarian.DataSource = dt;
            }

            decimal totalSeluruhPendapatan = 0;

            foreach (DataGridViewRow row in dataHarian.Rows)
            {
                if (row.Cells["total_pendapatan"].Value != DBNull.Value)
                {
                    totalSeluruhPendapatan += Convert.ToDecimal(row.Cells["total_pendapatan"].Value);
                }
            }


            dataHarian.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataHarian.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataHarian.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataHarian.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataHarian.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataHarian.Columns["tanggal"].HeaderText = "Tanggal";
            dataHarian.Columns["total_pendapatan"].HeaderText = "Total Pendapatan";
            dataHarian.Columns["jumlah_transaksi"].HeaderText = "Transaksi";

            dataHarian.Columns["total_pendapatan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataHarian.Columns["total_pendapatan"].DefaultCellStyle.Format = "N0";



            lblTotalBulan.Text = totalSeluruhPendapatan.ToString("N0");
        }
        private void DisplaySeluruhTransaksi()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tb_transaksi WHERE DATE(tanggal) = @tanggal ORDER BY transaksi_id", connection);
                cmd.Parameters.AddWithValue("@tanggal", selectedDate);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataDetailTransaksi.DataSource = dt;
            }

            DataDetailTransaksi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataDetailTransaksi.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            DataDetailTransaksi.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            DataDetailTransaksi.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            DataDetailTransaksi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DataDetailTransaksi.Columns["transaksi_id"].HeaderText = "ID Transaksi";
            DataDetailTransaksi.Columns["tanggal"].HeaderText = "Tanggal";
            DataDetailTransaksi.Columns["grand_total"].HeaderText = "Total";

            DataDetailTransaksi.Columns["grand_total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataDetailTransaksi.Columns["grand_total"].DefaultCellStyle.Format = "N0";

        }



        private void DataDetailTransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string transaksiId = DataDetailTransaksi.Rows[e.RowIndex].Cells["transaksi_id"].Value.ToString();
                TampilkanStruk(transaksiId);
            }
        }

        private void TampilkanStruk(string transaksiId)
        {
            StringBuilder struk = new StringBuilder();

            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();

                // Ambil informasi transaksi utama
                string queryTransaksi = @"
            SELECT t.tanggal, t.uang_masuk, t.kembalian, t.grand_total, t.metode_pembayaran, u.nama
            FROM tb_transaksi t
            JOIN tb_user u ON t.user_id = u.user_id
            WHERE t.transaksi_id = @transaksi_id";

                MySqlCommand cmdTransaksi = new MySqlCommand(queryTransaksi, conn);
                cmdTransaksi.Parameters.AddWithValue("@transaksi_id", transaksiId);

                string tanggal = "", namaKasir = "", metode = "";
                int total = 0, uangMasuk = 0, kembalian = 0;

                using (var reader = cmdTransaksi.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tanggal = Convert.ToDateTime(reader["tanggal"]).ToString("dd-MM-yyyy");
                        namaKasir = reader["nama"].ToString();
                        total = Convert.ToInt32(reader["grand_total"]);
                        uangMasuk = Convert.ToInt32(reader["uang_masuk"]);
                        kembalian = Convert.ToInt32(reader["kembalian"]);
                        metode = reader["metode_pembayaran"].ToString();
                    }
                }

                struk.AppendLine("======== STRUK PEMBAYARAN ========");
                struk.AppendLine("    - Toko KasMin Sejahtera - ");
                struk.AppendLine("                                ");
                struk.AppendLine($" Tanggal      : {tanggal}");
                struk.AppendLine($" Transaksi ID : {transaksiId}");
                struk.AppendLine($" Kasir        : {namaKasir}");
                struk.AppendLine("---------------------------------");
                struk.AppendLine(" Item            Qty   Subtotal");
                struk.AppendLine("---------------------------------");

                // Ambil detail produk
                string queryDetail = @"
            SELECT p.nama_produk, d.jumlah, d.subtotal
            FROM tb_detail_transaksi d
            JOIN tb_produk p ON d.produk_id = p.produk_id
            WHERE d.transaksi_id = @transaksi_id";

                MySqlCommand cmdDetail = new MySqlCommand(queryDetail, conn);
                cmdDetail.Parameters.AddWithValue("@transaksi_id", transaksiId);

                using (var reader = cmdDetail.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nama = reader["nama_produk"].ToString();
                        int jumlah = Convert.ToInt32(reader["jumlah"]);
                        int subtotal = Convert.ToInt32(reader["subtotal"]);

                        struk.AppendLine($"{nama.PadRight(15).Substring(0, 15)} {jumlah,3}  Rp{subtotal,8:N0}");
                    }
                }

                struk.AppendLine("---------------------------------");
                struk.AppendLine($"      Total        : Rp {total:N0}");
                struk.AppendLine($"      Uang Masuk   : Rp {uangMasuk:N0}");
                struk.AppendLine($"      Kembalian    : Rp {kembalian:N0}");
                struk.AppendLine($"      Metode Bayar : {metode}");
                struk.AppendLine("=================================");
            }

            strukToPrint = struk.ToString();
            printPreviewDialog.ShowDialog();
        }


        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 11, FontStyle.Regular);
            e.Graphics.DrawString(strukToPrint, font, Brushes.Black, new RectangleF(10, 10, 350, 1000));
        }

        private void cmbBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBulan.SelectedItem != null)
            {
                if (cmbBulan.SelectedItem.ToString() == "All")
                {
                    DisplayDataHarian();
                }
                else
                {
                    var selected = (KeyValuePair<string, string>)cmbBulan.SelectedItem;
                    var parts = selected.Key.Split('-');
                    int bulan = int.Parse(parts[0]);
                    int tahun = int.Parse(parts[1]);

                    DisplayDataHarianPerBulanTahun(bulan, tahun);
                }
            }
        }


        private void DisplayDataHarianPerBulanTahun(int bulan, int tahun)
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM view_pendapatan_harian WHERE MONTH(tanggal) = @bulan AND YEAR(tanggal) = @tahun", connection);
                cmd.Parameters.AddWithValue("@bulan", bulan);
                cmd.Parameters.AddWithValue("@tahun", tahun);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataHarian.DataSource = dt;
            }

            decimal totalPendapatanBulan = 0;
            foreach (DataGridViewRow row in dataHarian.Rows)
            {
                if (row.Cells["total_pendapatan"].Value != DBNull.Value)
                {
                    totalPendapatanBulan += Convert.ToDecimal(row.Cells["total_pendapatan"].Value);
                }
            }

            lblTotalBulan.Text = totalPendapatanBulan.ToString("N0");
        }


        private void HitungTotalKeseluruhan()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT SUM(total_pendapatan) FROM view_pendapatan_harian", connection);
                object result = cmd.ExecuteScalar();
                decimal total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                lbTotalKeseluruhan.Text = total.ToString("N0");
            }
        }

    }
}
