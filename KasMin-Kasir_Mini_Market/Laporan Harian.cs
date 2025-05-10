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
            DisplayDataHarian();
        }

        private void DisplayDataHarian()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM view_pendapatan_harian", connection);
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

            lblTotal.Text = totalSeluruhPendapatan.ToString("N0");
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
        }


        //private void DisplayDataDetail()
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
        //    {
        //        connection.Open();
        //        string query = @"
        //    SELECT t.transaksi_id, t.tanggal, p.nama_produk, d.jumlah, d.subtotal 
        //    FROM tb_transaksi t
        //    JOIN tb_detail_transaksi d ON t.transaksi_id = d.transaksi_id
        //    JOIN tb_produk p ON d.produk_id = p.produk_id
        //    ";

        //        MySqlCommand cmd = new MySqlCommand(query, connection);
               

        //        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        adapter.Fill(dt);
        //        DataDetailTransaksi.DataSource = dt;
        //    }
        //}

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
            struk.AppendLine("         STRUK TRANSAKSI        ");
            struk.AppendLine("================================");
            struk.AppendLine($"Tanggal       : {DateTime.Now:dd-MM-yyyy HH:mm}");
            struk.AppendLine($"Transaksi ID  : {transaksiId}");
            struk.AppendLine("--------------------------------");

            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                string query = @"
            SELECT p.nama_produk, d.jumlah, d.subtotal
            FROM tb_detail_transaksi d
            JOIN tb_produk p ON d.produk_id = p.produk_id
            WHERE d.transaksi_id = @transaksi_id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@transaksi_id", transaksiId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nama = reader["nama_produk"].ToString();
                        int jumlah = Convert.ToInt32(reader["jumlah"]);
                        int subtotal = Convert.ToInt32(reader["subtotal"]);

                        struk.AppendLine($"{nama,-18} x{jumlah,-2} Rp{subtotal,10:N0}");
                    }
                }
            }

            struk.AppendLine("================================");
            struk.AppendLine("     Terima Kasih Telah Belanja!");

            strukToPrint = struk.ToString();

            // Tampilkan preview cetak
            printPreviewDialog.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 11, FontStyle.Regular);
            e.Graphics.DrawString(strukToPrint, font, Brushes.Black, new RectangleF(10, 10, 280, 1000));
        }


    }
}
