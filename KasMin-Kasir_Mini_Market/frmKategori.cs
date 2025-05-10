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

namespace KasMin_Kasir_Mini_Market
{
    public partial class frmKategori : Form
    {
        public frmKategori()
        {
            InitializeComponent();
        }

        private void frmKategori_Load(object sender, EventArgs e)
        {
            generateID();
            displayData();
            txtKategoriId.Enabled = true;
        }

        private void displayData()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tb_kategori", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataKategori.DataSource = dt;
            }
            if (txtKategoriId.Text == "")
            {
                btnTambah.Text = "Tambah";
                return;
            }
        }

        private void generateID()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT MAX(kategori_id) FROM tb_kategori", connection);
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value && result.ToString().StartsWith("KTG"))
                {
                    string maxID = result.ToString();
                    int idNumber = int.Parse(maxID.Substring(3)); // Ambil angka setelah "KTG"
                    txtKategoriId.Text = "KTG" + (idNumber + 1).ToString("D2"); // Format 2 digit: KTG01, KTG02, dst.
                }
                else
                {
                    txtKategoriId.Text = "KTG01";
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if(btnTambah.Text == "Tambah")
            {
              using(MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO tb_kategori (kategori_id, nama_kategori) VALUES (@kategori_id, @nama_kategori)", connection);
                    cmd.Parameters.AddWithValue("@kategori_id", txtKategoriId.Text);
                    cmd.Parameters.AddWithValue("@nama_kategori", txtNamaKategori.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil ditambahkan!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayData();
                    generateID();
                }
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE tb_kategori SET nama_kategori = @nama_kategori WHERE kategori_id = @kategori_id", connection);
                    cmd.Parameters.AddWithValue("@kategori_id", txtKategoriId.Text);
                    cmd.Parameters.AddWithValue("@nama_kategori", txtNamaKategori.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diupdate!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayData();
                    generateID();
                }
                
                btnTambah.Text = "Tambah"; // Reset button text
            }
        }

        private void dataKategori_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataKategori.Rows.Count)
            {
                // Klik pada baris valid, isi data ke TextBox
                txtKategoriId.Text = dataKategori.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNamaKategori.Text = dataKategori.Rows[e.RowIndex].Cells[1].Value.ToString();

                btnTambah.Text = "Update";
                btnBatal.Text = "Hapus";
            }
            else
            {
                // Klik di area kosong, generate ID baru
                generateID();
                txtNamaKategori.Clear();
                btnTambah.Text = "Tambah";
                btnBatal.Text = "Batal";
            }
            if (txtKategoriId.Text == "")
            {
                generateID();
                btnTambah.Text = "Tambah";
                btnBatal.Text = "Batal";
                return;
            }
        }


        private void btnBatal_Click(object sender, EventArgs e)
        {
          if(btnBatal.Text == "Batal")
            {
                txtKategoriId.Clear();
                txtNamaKategori.Clear();
                btnTambah.Text = "Tambah";
                generateID();
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                    {
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM tb_kategori WHERE kategori_id = @kategori_id", connection);
                        cmd.Parameters.AddWithValue("@kategori_id", txtKategoriId.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        displayData();
                        generateID();
                    }

                }
                btnBatal.Text = "Batal";    
            }
        }
    }
}
