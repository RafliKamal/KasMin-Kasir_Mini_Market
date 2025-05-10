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
using com.google.zxing;

namespace KasMin_Kasir_Mini_Market
{
    public partial class frmTransaksi : Form
    {
        private FilterInfoCollection CaptureDevices;
        private VideoCaptureDevice videoSource;
        private bool barcodeDetected = false;
        private readonly object cameraLock = new object();

        string Barcode_produk = "";
        int TotalBayar = 0;
  

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (this.IsDisposed || !this.IsHandleCreated || picBarcode.IsDisposed)
                return;

            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            try
            {
                if (picBarcode.InvokeRequired)
                {
                    if (!picBarcode.IsDisposed && picBarcode.IsHandleCreated)
                    {
                        try
                        {
                            picBarcode.Invoke(new MethodInvoker(() =>
                            {
                                if (!picBarcode.IsDisposed && picBarcode.IsHandleCreated)
                                {
                                    picBarcode.Image?.Dispose();
                                    picBarcode.Image = (Bitmap)frame.Clone();
                                }
                            }));
                        }
                        catch (ObjectDisposedException) { /* Safe ignore */ }
                    }
                }
                else
                {
                    if (!picBarcode.IsDisposed && picBarcode.IsHandleCreated)
                    {
                        try
                        {
                            picBarcode.Image?.Dispose();
                            picBarcode.Image = (Bitmap)frame.Clone();
                        }
                        catch (ObjectDisposedException) { /* Safe ignore */ }
                    }
                }


                if (barcodeDetected)
                    return;

                try
                {
                    BarcodeDecoder decoder = new BarcodeDecoder();
                    var result = decoder.Decode(frame);

                    if (result != null)
                    {
                        barcodeDetected = true;

                        if (txtBarcode.InvokeRequired)
                        {
                            if (!txtBarcode.IsDisposed && txtBarcode.IsHandleCreated)
                            {
                                txtBarcode.Invoke(new MethodInvoker(() =>
                                {
                                    if (!txtBarcode.IsDisposed)
                                        txtBarcode.Text = result.Text;
                                    Barcode_produk = result.Text;
                                    TampilkanProdukDariBarcode(Barcode_produk); // Tambahkan baris ini

                                }));
                            }
                        }
                        else
                        {
                            if (!txtBarcode.IsDisposed)
                                txtBarcode.Text = result.Text;


                        }

                        // TODO: aksi setelah barcode dikenali
                    }
                }
                catch
                {
                    // ignore decode error
                }
            }
            finally
            {
                frame.Dispose();
            }
        }


        private void StartCamera()
        {
            if (CaptureDevices.Count == 0)
            {
                MessageBox.Show("Tidak ada perangkat kamera yang terdeteksi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (videoSource != null && videoSource.IsRunning)
            {
                StopCameraAsync();
            }

            videoSource = new VideoCaptureDevice(CaptureDevices[cb_camera.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start();
            barcodeDetected = false;
        }


        private async Task StopCameraAsync()
        {
            VideoCaptureDevice tempVideoSource = null;

            lock (cameraLock)
            {
                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.NewFrame -= VideoSource_NewFrame; // cabut handler
                    videoSource.SignalToStop();
                    tempVideoSource = videoSource;
                    videoSource = null;
                }

                // Bersihkan gambar di dalam lock
                if (picBarcode.InvokeRequired)
                {
                    if (!picBarcode.IsDisposed && picBarcode.IsHandleCreated)
                    {
                        picBarcode.Invoke(new MethodInvoker(() =>
                        {
                            if (!picBarcode.IsDisposed)
                                picBarcode.Image = null;
                        }));
                    }
                }
                else
                {
                    if (!picBarcode.IsDisposed)
                        picBarcode.Image = null;
                }
            }

            // Tunggu stop kamera di luar lock
            if (tempVideoSource != null)
            {
                await Task.Run(() =>
                {
                    try { tempVideoSource.WaitForStop(); } catch { }
                });
            }
        }







        public string CurrentUserId { get; set; } // Set saat form ini dipanggil
        public string NamaKasir { get; set; } // Set saat form ini dipanggil

        public frmTransaksi()
        {
            InitializeComponent();
            this.FormClosing += frmTransaksi_FormClosing;
        }

        private void frmTransaksi_Load(object sender, EventArgs e)
        {

            // Inisialisasi lainnya
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (CaptureDevices.Count == 0)
            {
                MessageBox.Show("Tidak ada kamera yang tersedia.");
                return;
            }

            foreach (FilterInfo device in CaptureDevices)
            {
                cb_camera.Items.Add(device.Name);
            }

            cb_camera.SelectedIndex = 1;
            cb_camera.SelectedIndexChanged += cb_camera_SelectedIndexChanged;

            StartCamera();

            // Inisialisasi transaksi & UI
            NamaKategori();
           
            txtNamaKasir.Text = NamaKasir;
            txtTransaksiId.Enabled = false;
            txtNamaKasir.Enabled = false;
            txtBarcode.Enabled = false;
            btnHapus.Enabled = false;
            btnUpdate.Enabled = false;
            txtBarcode.Text = "";
            cmbNamaKategori.SelectedIndex = -1;
            cmbNamaProduk.SelectedIndex = -1;
            clearField();
            DisplayData();
        }

        private void CekTransaksiAktif()
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();

                // 1. Cek apakah sudah ada transaksi yang belum selesai
                string cekQuery = @"SELECT transaksi_id 
                                FROM tb_transaksi 
                                WHERE metode_pembayaran IS NULL 
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
                    string insertQuery = @"INSERT INTO tb_transaksi (transaksi_id, user_id) 
                                       VALUES (@id, @userId)";

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
            if (isProgrammaticChange) return;

            if (cmbNamaProduk.SelectedItem != null)
            {
                DataRowView selectedRow = cmbNamaProduk.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    txtBarcode.Text = selectedRow["barcode"].ToString();
                    Barcode_produk = txtBarcode.Text;
                    TampilkanProdukDariBarcode(Barcode_produk);
                }
            }
        }


        private void cb_camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartCamera();
        }

        private async void frmTransaksi_FormClosing(object sender, FormClosingEventArgs e)
        {
            await StopCameraAsync();
        }

        string hargaproduk = "";

        private bool isProgrammaticChange = false;

        private void TampilkanProdukDariBarcode(string barcode)
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                string query = @"SELECT p.produk_id, p.nama_produk, p.kategori_id, p.stok, p.harga, k.nama_kategori
                         FROM tb_produk p
                         JOIN tb_kategori k ON p.kategori_id = k.kategori_id
                         WHERE p.barcode = @barcode";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string kategoriId = reader["kategori_id"].ToString();
                            string produkId = reader["produk_id"].ToString();
                            string namaProduk = reader["nama_produk"].ToString();
                            string namaKategori = reader["nama_kategori"].ToString();
                            string stok = reader["stok"].ToString();
                            hargaproduk = reader["harga"].ToString();

                            lblStok.Text = stok;


                            isProgrammaticChange = true;
                            cmbNamaKategori.SelectedValue = kategoriId;
                            LoadProdukBerdasarkanKategori(kategoriId);
                            cmbNamaProduk.SelectedValue = produkId;
                            isProgrammaticChange = false;

                        }
                    }
                }
            }
        }

        private void btnTambahProduk_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text == "")
            {
                MessageBox.Show("Jumlah produk tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (int.Parse(txtQuantity.Text) > int.Parse(lblStok.Text))
            {
                MessageBox.Show("Jumlah produk melebihi stok yang tersedia!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int jumlahBaru = int.Parse(txtQuantity.Text);
            int hargaSatuan = int.Parse(hargaproduk);
            int subtotalBaru = jumlahBaru * hargaSatuan;

            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();

                // 1. Cek apakah data sudah ada
                string cekQuery = "SELECT jumlah, subtotal FROM tb_detail_transaksi WHERE transaksi_id = @transaksiId AND produk_id = @produkId";
                MySqlCommand cekCmd = new MySqlCommand(cekQuery, conn);
                cekCmd.Parameters.AddWithValue("@transaksiId", txtTransaksiId.Text);
                cekCmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                MySqlDataReader reader = cekCmd.ExecuteReader();

                bool dataAda = false;
                int jumlahLama = 0;
                int subtotalLama = 0;

                if (reader.Read())
                {
                    dataAda = true;
                    jumlahLama = int.Parse(reader["jumlah"].ToString());
                    subtotalLama = int.Parse(reader["subtotal"].ToString());
                }
                reader.Close();

                // 2. Update stok produk
                string updateStokQuery = "UPDATE tb_produk SET stok = stok - @jumlah WHERE produk_id = @produkId";
                MySqlCommand updateStokCmd = new MySqlCommand(updateStokQuery, conn);
                updateStokCmd.Parameters.AddWithValue("@jumlah", jumlahBaru);
                updateStokCmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                updateStokCmd.ExecuteNonQuery();


                if (dataAda)
                {
                    // 3a. Jika sudah ada, update jumlah dan subtotal
                    int jumlahTotal = jumlahLama + jumlahBaru;
                    int subtotalTotal = subtotalLama + subtotalBaru;

                    string updateQuery = @"UPDATE tb_detail_transaksi 
                                   SET jumlah = @jumlah, subtotal = @subtotal 
                                   WHERE transaksi_id = @transaksiId AND produk_id = @produkId";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@jumlah", jumlahTotal);
                    updateCmd.Parameters.AddWithValue("@subtotal", subtotalTotal);
                    updateCmd.Parameters.AddWithValue("@transaksiId", txtTransaksiId.Text);
                    updateCmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    // 3b. Jika belum ada, insert data baru
                    string insertQuery = @"INSERT INTO tb_detail_transaksi (transaksi_id, produk_id, jumlah, subtotal)
                                   VALUES (@transaksiId, @produkId, @jumlah, @subtotal)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@transaksiId", txtTransaksiId.Text);
                    insertCmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@jumlah", jumlahBaru);
                    insertCmd.Parameters.AddWithValue("@subtotal", subtotalBaru);
                    insertCmd.ExecuteNonQuery();
                }
            }
            // Update lblStok dengan stok terbaru
            int stokLama = int.Parse(lblStok.Text);
            int stokBaru = stokLama - jumlahBaru;
            lblStok.Text = stokBaru.ToString();
            DisplayData();
            clearField();
            MessageBox.Show("Produk berhasil ditambahkan/diupdate dalam transaksi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DisplayData()
        {
            using (MySqlConnection detail = new MySqlConnection(Koneksi.Connect))
            {
                detail.Open();
                string query = @"
        SELECT 
            t.produk_id, 
            p.nama_produk AS 'Nama Produk', 
            t.jumlah AS 'Jumlah', 
            t.subtotal AS 'Subtotal' 
        FROM 
            tb_detail_transaksi t 
        JOIN 
            tb_produk p ON t.produk_id = p.produk_id 
        WHERE 
            t.transaksi_id = @transaksi_id";

                MySqlDataAdapter da = new MySqlDataAdapter(query, detail);
                da.SelectCommand.Parameters.AddWithValue("@transaksi_id", txtTransaksiId.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridTransaksi.DataSource = dt;

                // Sembunyikan kolom produk_id
                if (dataGridTransaksi.Columns.Contains("produk_id"))
                {
                    dataGridTransaksi.Columns["produk_id"].Visible = false;
                }

                // Hitung total subtotal
                int total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToInt32(row["Subtotal"]);
                }
                TotalBayar = total;
                // Tampilkan total ke label
                lblTotal.Text = $"Rp {total:N0}";
            }
        }



        private void dataGridTransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Cegah klik header

            var row = dataGridTransaksi.Rows[e.RowIndex];

            // Pastikan nilai tidak null
            if (row.Cells[0].Value == null || row.Cells[2].Value == null)
            {
                clearField();
                return;
            }
            else
            {
                btnHapus.Enabled = true;
                btnUpdate.Enabled = true;
                string produk_id = dataGridTransaksi.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtQuantity.Text = dataGridTransaksi.Rows[e.RowIndex].Cells[2].Value.ToString();

                using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
                {
                    conn.Open();
                    string query = @"
            SELECT 
                p.nama_produk, 
                p.kategori_id, 
                p.stok, 
                k.nama_kategori 
            FROM 
                tb_produk p
            JOIN 
                tb_kategori k ON p.kategori_id = k.kategori_id
            WHERE 
                p.produk_id = @produk_id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@produk_id", produk_id);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string kategori_id = reader["kategori_id"].ToString();
                        cmbNamaKategori.Text = reader["nama_kategori"].ToString();
                        cmbNamaProduk.Text = reader["nama_produk"].ToString();
                        lblStok.Text = reader["stok"].ToString();
                    }
                }
            }



        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuantity.Text) ||
              string.IsNullOrWhiteSpace(txtTransaksiId.Text) ||
              cmbNamaProduk.SelectedIndex == -1)
            {
                MessageBox.Show("Harap Pilih data terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus produk ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                {
                    connection.Open();

                    // 1. Ambil jumlah dari detail transaksi
                    int jumlah = 0;
                    MySqlCommand cmdJumlah = new MySqlCommand("SELECT jumlah FROM tb_detail_transaksi WHERE produk_id = @produk_id AND transaksi_id = @transaksi_id", connection);
                    cmdJumlah.Parameters.AddWithValue("@produk_id", cmbNamaProduk.SelectedValue);
                    cmdJumlah.Parameters.AddWithValue("@transaksi_id", txtTransaksiId.Text);
                    object result = cmdJumlah.ExecuteScalar();

                    if (result != null)
                        jumlah = Convert.ToInt32(result);

                    // 2. Update stok di tb_produk
                    MySqlCommand cmdUpdateStok = new MySqlCommand("UPDATE tb_produk SET stok = stok + @jumlah WHERE produk_id = @produk_id", connection);
                    cmdUpdateStok.Parameters.AddWithValue("@jumlah", jumlah);
                    cmdUpdateStok.Parameters.AddWithValue("@produk_id", cmbNamaProduk.SelectedValue);
                    cmdUpdateStok.ExecuteNonQuery();

                    // 3. Hapus data dari detail transaksi
                    MySqlCommand cmdDelete = new MySqlCommand("DELETE FROM tb_detail_transaksi WHERE produk_id = @produk_id AND transaksi_id = @transaksi_id", connection);
                    cmdDelete.Parameters.AddWithValue("@produk_id", cmbNamaProduk.SelectedValue);
                    cmdDelete.Parameters.AddWithValue("@transaksi_id", txtTransaksiId.Text);
                    cmdDelete.ExecuteNonQuery();
                }


                MessageBox.Show("Data produk berhasil dihapus dan stok dikembalikan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            clearField();


            DisplayData();
            btnHapus.Enabled = false;
        }


        public void clearField()
        {
            CekTransaksiAktif();
            txtBarcode.Text = "";
            cmbNamaKategori.SelectedIndex = -1;
            cmbNamaProduk.SelectedIndex = -1;
            txtQuantity.Text = "";
            lblStok.Text = "0";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtTransaksiId.Text) ||
                cmbNamaProduk.SelectedIndex == -1)
            {
                MessageBox.Show("Harap Pilih data terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int jumlahBaru = int.Parse(txtQuantity.Text);
            int hargaSatuan = int.Parse(hargaproduk);
            int subtotalBaru = jumlahBaru * hargaSatuan;
            int jumlahLama = 0;
            int stokSaatIni = 0;

            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();

                // Ambil jumlah lama
                string cekQuery = @"SELECT jumlah FROM tb_detail_transaksi 
                    WHERE transaksi_id = @transaksiId AND produk_id = @produkId";
                MySqlCommand cekCmd = new MySqlCommand(cekQuery, conn);
                cekCmd.Parameters.AddWithValue("@transaksiId", txtTransaksiId.Text);
                cekCmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                var result = cekCmd.ExecuteScalar();
                if (result != null)
                    jumlahLama = Convert.ToInt32(result);

                int selisih = jumlahBaru - jumlahLama;

                // Ambil stok saat ini
                string stokQuery = "SELECT stok FROM tb_produk WHERE produk_id = @produkId";
                MySqlCommand stokCmd = new MySqlCommand(stokQuery, conn);
                stokCmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                stokSaatIni = Convert.ToInt32(stokCmd.ExecuteScalar());

                // Cek apakah stok mencukupi
                if (selisih > 0 && selisih > stokSaatIni)
                {
                    MessageBox.Show("Stok tidak mencukupi untuk update jumlah produk ini.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update stok
                string updateStokQuery = "UPDATE tb_produk SET stok = stok - @selisih WHERE produk_id = @produkId";
                MySqlCommand updateStokCmd = new MySqlCommand(updateStokQuery, conn);
                updateStokCmd.Parameters.AddWithValue("@selisih", selisih);
                updateStokCmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                updateStokCmd.ExecuteNonQuery();

                // Update atau insert detail transaksi
                string query = (result != null)
                    ? @"UPDATE tb_detail_transaksi 
        SET jumlah = @jumlah, subtotal = @subtotal 
        WHERE transaksi_id = @transaksiId AND produk_id = @produkId"
                    : @"INSERT INTO tb_detail_transaksi 
        (transaksi_id, produk_id, jumlah, subtotal) 
        VALUES (@transaksiId, @produkId, @jumlah, @subtotal)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@transaksiId", txtTransaksiId.Text);
                cmd.Parameters.AddWithValue("@produkId", cmbNamaProduk.SelectedValue);
                cmd.Parameters.AddWithValue("@jumlah", jumlahBaru);
                cmd.Parameters.AddWithValue("@subtotal", subtotalBaru);
                cmd.ExecuteNonQuery();
            }

            // Update label stok di UI
            int stokLama = int.Parse(lblStok.Text);
            lblStok.Text = (stokLama - (jumlahBaru - jumlahLama)).ToString();

            btnUpdate.Enabled = false;
            clearField();
            DisplayData();
            MessageBox.Show("Produk berhasil diperbarui.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnBatalkan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda yakin ingin membatalkan seluruh transaksi ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
                {
                    conn.Open();

                    // 1. Ambil semua produk dan jumlah dari transaksi
                    string ambilQuery = @"SELECT produk_id, jumlah 
                                  FROM tb_detail_transaksi 
                                  WHERE transaksi_id = @transaksi_id";
                    MySqlCommand ambilCmd = new MySqlCommand(ambilQuery, conn);
                    ambilCmd.Parameters.AddWithValue("@transaksi_id", txtTransaksiId.Text);

                    List<(string produkId, int jumlah)> daftarProduk = new List<(string, int)>();
                    using (var reader = ambilCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string produkId = reader["produk_id"].ToString();
                            int jumlah = Convert.ToInt32(reader["jumlah"]);
                            daftarProduk.Add((produkId, jumlah));
                        }
                    }

                    // 2. Kembalikan stok untuk setiap produk
                    foreach (var (produkId, jumlah) in daftarProduk)
                    {
                        string updateStok = @"UPDATE tb_produk 
                                      SET stok = stok + @jumlah 
                                      WHERE produk_id = @produk_id";
                        MySqlCommand cmdStok = new MySqlCommand(updateStok, conn);
                        cmdStok.Parameters.AddWithValue("@jumlah", jumlah);
                        cmdStok.Parameters.AddWithValue("@produk_id", produkId);
                        cmdStok.ExecuteNonQuery();
                    }

                    // 3. Hapus semua detail transaksi
                    string hapusQuery = @"DELETE FROM tb_detail_transaksi 
                                  WHERE transaksi_id = @transaksi_id";
                    MySqlCommand hapusCmd = new MySqlCommand(hapusQuery, conn);
                    hapusCmd.Parameters.AddWithValue("@transaksi_id", txtTransaksiId.Text);
                    hapusCmd.ExecuteNonQuery();
                }

                DisplayData();
                lblTotal.Text = "Rp 0";
                clearField();
                MessageBox.Show("Transaksi berhasil dibatalkan dan stok produk dikembalikan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {

           
            frmBayar bayar = new frmBayar();
            bayar.TransaksiId = txtTransaksiId.Text;
            bayar.Total = TotalBayar.ToString();
            bayar.NamaKasir = txtNamaKasir.Text;
            bayar.tanggal = dtpTanggal.Value.ToString("yyyy-MM-dd");
            bayar.ShowDialog();
        }
    }
}
