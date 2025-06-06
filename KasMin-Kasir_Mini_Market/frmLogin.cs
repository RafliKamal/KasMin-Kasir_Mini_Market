using Microsoft.VisualBasic.Logging;
using MySqlConnector;

namespace KasMin_Kasir_Mini_Market
{
    public partial class frmLogin : Form
    {
        int login = 0;
        public frmLogin()
        {
            InitializeComponent();
            Panel shadowPanel = new Panel();
            shadowPanel.Size = btnLogin.Size;
            shadowPanel.Location = new Point(btnLogin.Left + 2, btnLogin.Top + 2); // Geser sedikit untuk efek shadow
            shadowPanel.BackColor = Color.Gray; // Warna shadow
            shadowPanel.Enabled = false; // Biar nggak mengganggu klik
            shadowPanel.SendToBack(); // Pastikan ada di belakang tombol

            this.Controls.Add(shadowPanel);
            btnLogin.BringToFront(); // Tombol tetap di atas

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            while (login < 3)
            {
                if ((txtUsername.Text.Length == 0) || (txtPassword.Text.Length == 0))
                {
                    MessageBox.Show("Username dan Password tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM tb_user WHERE nama = @username AND password = @password", connection);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string role = reader["role"].ToString();
                        string userId = reader["user_id"].ToString(); // Ambil user_id dari database
                        string nama = reader["nama"].ToString();

                        MessageBox.Show("Selamat Datang " + txtUsername.Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        frmDashboard frmDash = new frmDashboard();
                        frmDash.UserId = userId; // Kirim ke Dashboard
                        frmDash.Nama = nama; // Kirim nama ke Dashboard
                        frmDash.Text = "Dashboard " + nama;


                        frmDash.Show();
                        if(role == "Admin")
                        {
                            frmDash.userToolStripMenuItem.Visible = true;
                            frmDash.kategoriToolStripMenuItem1.Visible = true;
                            frmDash.produkToolStripMenuItem1.Visible = true;
                            frmDash.laporanPenjualanToolStripMenuItem.Visible = true;
                        }
                        else
                        {
                            frmDash.userToolStripMenuItem.Visible = false;
                            frmDash.kategoriToolStripMenuItem1.Visible = false;
                            frmDash.produkToolStripMenuItem1.Visible = false;
                            frmDash.laporanPenjualanToolStripMenuItem.Visible = false;
                        }
                        break;
                    }
                    else
                    {
                        login++;
                        MessageBox.Show("Username dan Password Tidak Valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                        if (login == 2)
                        {
                            MessageBox.Show("Anda telah 2x salah login, 1 kesempatan lagi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (login == 3)
                        {
                            MessageBox.Show("Anda telah 3x salah login, Aplikasi Akan Dikunci", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPassword.Text = "";
                            txtUsername.Text = "";
                            btnLogin.Enabled = false;
                            txtUsername.Enabled = false;
                            txtPassword.Enabled = false;
                        }
                        return;
                    }
                }
            }
        }
    }
}
