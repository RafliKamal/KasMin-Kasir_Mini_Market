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
                        MessageBox.Show("Selamat Datang " + txtUsername.Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        frmDashboard frmDash = new frmDashboard();
                        frmDash.Show();
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
