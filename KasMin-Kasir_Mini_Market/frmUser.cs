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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void DisplayData()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tb_user", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            GenerateUserId();





        }
        private void GenerateUserId()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT MAX(user_id) FROM tb_user", connection);
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value && result.ToString().StartsWith("USR"))
                {
                    string maxID = result.ToString();
                    int idNumber = int.Parse(maxID.Substring(3)); // Ambil angka setelah "USR"
                    txtUserId.Text = "USR" + (idNumber + 1).ToString("D2"); // Format 2 digit: USR01, USR02, dst.
                }
                else
                {
                    txtUserId.Text = "USR01";
                }
            }
        }

        private void clearFields()
        {
            txtUserId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1; // Reset ComboBox selection
            btnTambah.Text = "Tambah"; // Reset button text to "Tambah"

            GenerateUserId(); // Generate new User ID   
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            clearFields();
            DisplayData();

            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Kasir");

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].FillWeight = 50; // Kolom 0 lebih kecil
            dataGridView1.Columns[1].FillWeight = 100; // Kolom 1 lebih besar


            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            txtUserId.Enabled = false; // Disable the User ID text box


            // Set fokus awal ke txtUsername
            this.ActiveControl = txtUsername;
        }



        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Semua field harus diisi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnTambah.Text == "Update")
            {
                UpdateUser();
            }
            else
            {
                InsertUser();
            }


            clearFields();
        }
        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (btnBatal.Text == "Hapus")
            {
                DeleteUser();
                btnBatal.Text = "Batal";
            }
            else
            {
                clearFields();
                btnTambah.Text = "Tambah";

            }

        }
        private void UpdateUser()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tb_user SET nama = @username, password = @password, role = @role WHERE user_id = @userId", connection);
                cmd.Parameters.AddWithValue("@userId", txtUserId.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Data berhasil diupdate!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisplayData();
        }

        private void InsertUser()
        {
            using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO tb_user (user_id, nama, password, role) VALUES (@userId, @username, @password, @role)", connection);
                cmd.Parameters.AddWithValue("@userId", txtUserId.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Data berhasil ditambahkan!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisplayData();
        }

        private void DeleteUser()
        {
            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM tb_user WHERE user_id = @userId", connection);
                    cmd.Parameters.AddWithValue("@userId", txtUserId.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Data berhasil dihapus!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                txtUserId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmbRole.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                btnTambah.Text = "Update";
                btnBatal.Text = "Hapus";
            }
            if (txtUserId.Text == "")
            {
                // Jika tidak ada baris yang valid, reset semua field
                clearFields();
                GenerateUserId(); // Generate new User ID
                btnTambah.Text = "Tambah"; // Reset button text to "Tambah"
                btnBatal.Text = "Batal"; // Reset button text to "Batal"
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
