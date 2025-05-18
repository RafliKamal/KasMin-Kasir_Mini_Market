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
        private PrintDocument printDocumentStruk = new PrintDocument();
        private PrintDocument printDocumentLaporan = new PrintDocument();

        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();

        public Laporan_Harian()
        {
            InitializeComponent();

            // === PRINT STRUK ===
            printDocumentStruk.PrintPage += printDocumentStruk_PrintPage;
            PaperSize paperSizeStruk = new PaperSize("StrukCustom", 350, 600); 
            printDocumentStruk.DefaultPageSettings.PaperSize = paperSizeStruk;

            // === PRINT LAPORAN ===
            printDocumentLaporan.PrintPage += printDocumentLaporan_PrintPage;
            PaperSize paperSizeA4 = new PaperSize("A4", 700, 1200);
            printDocumentLaporan.DefaultPageSettings.PaperSize = paperSizeA4;

            // Preview Dialog Setup
            printPreviewDialog.Document = printDocumentStruk; // default, bisa diganti saat preview laporan
            printPreviewDialog.ClientSize = new Size(827, 1000);
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
            printPreviewDialog.Document = printDocumentStruk; // <-- arahkan ke dokumen struk
            printPreviewDialog.ClientSize = new Size(350, 600); // struk kecil
            printPreviewDialog.ShowDialog();

        }


        private void printDocumentStruk_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 11, FontStyle.Regular);
            e.Graphics.DrawString(strukToPrint, font, Brushes.Black, new RectangleF(10, 10, 350, 1000));
        }




        private void printDocumentLaporan_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 10);
            int margin = 40;
            RectangleF area = new RectangleF(margin, margin, e.MarginBounds.Width - margin * 2, e.MarginBounds.Height - margin * 2);
            e.Graphics.DrawString(strukToPrint, font, Brushes.Black, area);
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

        private void btnCetak_Click(object sender, EventArgs e)
        {
            StringBuilder laporan = new StringBuilder();

            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();

                // Ambil daftar bulan-tahun unik
                List<(int Bulan, int Tahun)> listBulanTahun = new List<(int, int)>();

                using (var cmdBT = new MySqlCommand(@"
            SELECT DISTINCT MONTH(tanggal) AS bulan, YEAR(tanggal) AS tahun
            FROM tb_transaksi
            WHERE tanggal IS NOT NULL
            ORDER BY tahun, bulan", conn))
                using (var readerBT = cmdBT.ExecuteReader())
                {
                    while (readerBT.Read())
                    {
                        listBulanTahun.Add((readerBT.GetInt32("bulan"), readerBT.GetInt32("tahun")));
                    }
                }

                decimal totalKeseluruhan = 0;

                foreach (var (bulan, tahun) in listBulanTahun)
                {
                    laporan.AppendLine("=============================================");
                    laporan.AppendLine($"        LAPORAN BULAN {new DateTime(tahun, bulan, 1):MMMM yyyy}".ToUpper());
                    laporan.AppendLine("=============================================");

                    // Ambil data harian
                    List<(DateTime Tanggal, int JumlahTransaksi, int TotalPendapatan)> listHarian = new List<(DateTime, int, int)>();

                    using (var cmdHarian = new MySqlCommand(@"
                SELECT DATE(tanggal) AS tanggal, COUNT(*) AS jumlah_transaksi, SUM(grand_total) AS total_pendapatan
                FROM tb_transaksi
                WHERE MONTH(tanggal) = @bulan AND YEAR(tanggal) = @tahun
                GROUP BY DATE(tanggal)
                ORDER BY tanggal", conn))
                    {
                        cmdHarian.Parameters.AddWithValue("@bulan", bulan);
                        cmdHarian.Parameters.AddWithValue("@tahun", tahun);

                        using (var readerHarian = cmdHarian.ExecuteReader())
                        {
                            while (readerHarian.Read())
                            {
                                DateTime tgl = readerHarian.GetDateTime("tanggal");
                                int trxCount = readerHarian.GetInt32("jumlah_transaksi");
                                int totalPendapatan = readerHarian.GetInt32("total_pendapatan");
                                listHarian.Add((tgl, trxCount, totalPendapatan));
                            }
                        }
                    }

                    foreach (var harian in listHarian)
                    {
                        laporan.AppendLine($"Tanggal          : {harian.Tanggal:dd-MM-yyyy}");
                        laporan.AppendLine($"Jumlah Transaksi : {harian.JumlahTransaksi}");
                        laporan.AppendLine($"Total Pendapatan : Rp {harian.TotalPendapatan:N0}");
                        laporan.AppendLine("Produk Terjual:");

                        using (var cmdDetailProduk = new MySqlCommand(@"
                    SELECT p.nama_produk, SUM(d.jumlah) AS qty, SUM(d.subtotal) AS subtotal
                    FROM tb_detail_transaksi d
                    JOIN tb_produk p ON d.produk_id = p.produk_id
                    JOIN tb_transaksi t ON d.transaksi_id = t.transaksi_id
                    WHERE DATE(t.tanggal) = @tanggal
                    GROUP BY d.produk_id
                    ORDER BY subtotal DESC", conn))
                        {
                            cmdDetailProduk.Parameters.AddWithValue("@tanggal", harian.Tanggal);

                            using (var readerProduk = cmdDetailProduk.ExecuteReader())
                            {
                                while (readerProduk.Read())
                                {
                                    string nama = readerProduk["nama_produk"].ToString();
                                    int qty = Convert.ToInt32(readerProduk["qty"]);
                                    int subtotal = Convert.ToInt32(readerProduk["subtotal"]);

                                    laporan.AppendLine($"  - {nama.PadRight(20).Substring(0, 20)} x{qty,2} = Rp {subtotal:N0}");
                                }
                            }
                        }

                        laporan.AppendLine(); // spasi antar hari
                    }

                    // Hitung total per bulan
                    using (var cmdTotalBulan = new MySqlCommand(@"
                SELECT SUM(grand_total) FROM tb_transaksi 
                WHERE MONTH(tanggal) = @bulan AND YEAR(tanggal) = @tahun", conn))
                    {
                        cmdTotalBulan.Parameters.AddWithValue("@bulan", bulan);
                        cmdTotalBulan.Parameters.AddWithValue("@tahun", tahun);

                        object result = cmdTotalBulan.ExecuteScalar();
                        decimal totalBulan = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                        laporan.AppendLine("---------------------------------------------");
                        laporan.AppendLine($"TOTAL PENJUALAN BULAN {new DateTime(tahun, bulan, 1):MMMM yyyy}: Rp {totalBulan:N0}");
                        laporan.AppendLine("---------------------------------------------\n");

                        totalKeseluruhan += totalBulan;
                    }
                }


                laporan.AppendLine("=============================================");
                laporan.AppendLine($"TOTAL PENJUALAN KESELURUHAN: Rp {totalKeseluruhan:N0}");
                laporan.AppendLine("=============================================");
            }

            strukToPrint = laporan.ToString();
            printPreviewDialog.Document = printDocumentLaporan;
            printPreviewDialog.ClientSize = new Size(827, 1000);
            printPreviewDialog.ShowDialog();
        }





    }
}
