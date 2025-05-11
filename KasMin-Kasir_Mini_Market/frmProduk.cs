using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    public partial class frmProduk : Form
    {
        public frmProduk()
        {

            InitializeComponent();

        }

        private FilterInfoCollection CaptureDevices;
        private VideoCaptureDevice videoSource;
        private bool barcodeDetected = false;

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (this.IsDisposed || this.Disposing) return;

            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            if (picBarcode.IsHandleCreated && !picBarcode.IsDisposed)
            {
                try
                {
                    picBarcode.Invoke(new MethodInvoker(delegate
                    {
                        picBarcode.Image?.Dispose();
                        picBarcode.Image = (Bitmap)frame.Clone();
                    }));
                }
                catch (ObjectDisposedException)
                {
                    // Ignore if picture box is already disposed
                }
            }

            if (barcodeDetected)
            {
                frame.Dispose();
                return;
            }

            try
            {
                BarcodeDecoder decoder = new BarcodeDecoder();
                var result = decoder.Decode(frame);

                if (result != null)
                {
                    barcodeDetected = true;

                    if (txtBarcode.IsHandleCreated && !txtBarcode.IsDisposed)
                    {
                        txtBarcode.Invoke(new MethodInvoker(delegate ()
                        {
                            txtBarcode.Text = result.Text;
                        }));
                    }

                    if (btnScanBarcode.IsHandleCreated && !btnScanBarcode.IsDisposed)
                    {
                        btnScanBarcode.Invoke(new MethodInvoker(() =>
                        {
                            btnScanBarcode.Text = "Stop Scan";
                        }));
                    }
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                frame.Dispose();
            }
        }



        private void DisplayData()
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM tb_produk", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridProduk.DataSource = dt;
            }
            if (btnScanBarcode.Text == "Stop Scan")
            {
                this.ControlBox = false;
            }
        }



        private void frmProduk_Load_1(object sender, EventArgs e)
        {
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (CaptureDevices.Count == 0)
            {
                MessageBox.Show("Tidak ada perangkat kamera yang terdeteksi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (FilterInfo device in CaptureDevices)
            {
                cb_camera.Items.Add(device.Name);
            }

            cb_camera.SelectedIndex = 2;
            btnScanBarcode.Text = "Stop Scan";
            videoSource = new VideoCaptureDevice(CaptureDevices[cb_camera.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start();
            DisplayData();

            btnBatal.Text = "Batal";
            btnSimpan.Text = "Simpan";
            GenerateProdukId();
            LoadKategori();


            dataGridProduk.Columns[0].HeaderText = "ID Produk";
            dataGridProduk.Columns[1].HeaderText = "ID Kategori";
            dataGridProduk.Columns[2].HeaderText = "Nama Produk";
            dataGridProduk.Columns[3].HeaderText = "Stok";
            dataGridProduk.Columns[4].HeaderText = "Harga";
            dataGridProduk.Columns[5].HeaderText = "Gambar Produk";
            dataGridProduk.Columns[6].HeaderText = "Barcode";
            dataGridProduk.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridProduk.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridProduk.ColumnHeadersHeight = 50; // Set tinggi header kolom
            dataGridProduk.RowHeadersVisible = false; // Sembunyikan header baris
            dataGridProduk.RowHeadersWidth = 50; // Set lebar header baris
            dataGridProduk.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridProduk.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridProduk.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridProduk.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridProduk.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridProduk.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridProduk.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridProduk.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            


        }

        private void GenerateProdukId()
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT MAX(produk_id) FROM tb_produk", conn);
                var result = cmd.ExecuteScalar()?.ToString();

                if (!string.IsNullOrEmpty(result))
                {
                    int nomor = int.Parse(result.Substring(3)) + 1;
                    txtProdukId.Text = "PRD" + nomor.ToString("D3");
                }
                else
                {
                    txtProdukId.Text = "PRD001";
                }
            }
        }

        private void LoadKategori()
        {
            using (MySqlConnection conn = new MySqlConnection(Koneksi.Connect))
            {
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT kategori_id, nama_kategori FROM tb_kategori", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbKategoriId.DataSource = dt;
                cmbKategoriId.DisplayMember = "nama_kategori";
                cmbKategoriId.ValueMember = "kategori_id";
            }
        }

        private void picProduk_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picProduk.ImageLocation = ofd.FileName;
            }
        }

        private void btnSimpan_Click_1(object sender, EventArgs e)
        {
            if (btnSimpan.Text == "Simpan")
            {
                if(txtStok.Text == "" || txtNamaProduk.Text == "" || txtHarga.Text == "" || cmbKategoriId.SelectedIndex == -1 || txtBarcode.Text == "")
                {
                    MessageBox.Show("Silahkan isi semua data produk!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                    {
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand(@"INSERT INTO tb_produk (produk_id, kategori_id, nama_produk, harga, stok, gambar_produk, barcode) 
                            VALUES (@produk_id, @kategori_id, @nama_produk, @harga, @stok, @gambar_produk, @barcode)", connection);
                        cmd.Parameters.AddWithValue("@produk_id", txtProdukId.Text);
                        cmd.Parameters.AddWithValue("@kategori_id", cmbKategoriId.SelectedValue);
                        cmd.Parameters.AddWithValue("@nama_produk", txtNamaProduk.Text);
                        cmd.Parameters.AddWithValue("@harga", int.Parse(txtHarga.Text));
                        cmd.Parameters.AddWithValue("@stok", int.Parse(txtStok.Text));
                        cmd.Parameters.AddWithValue("@gambar_produk", picProduk.ImageLocation ?? "");
                        cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data produk berhasil disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearField();
                }
            }
            else if (btnSimpan.Text == "Update")
            {
                using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(@"UPDATE tb_produk 
                        SET kategori_id = @kategori_id, nama_produk = @nama_produk, harga = @harga, stok = @stok, gambar_produk = @gambar_produk, barcode = @barcode 
                        WHERE produk_id = @produk_id", connection);
                    cmd.Parameters.AddWithValue("@produk_id", txtProdukId.Text);
                    cmd.Parameters.AddWithValue("@kategori_id", cmbKategoriId.SelectedValue);
                    cmd.Parameters.AddWithValue("@nama_produk", txtNamaProduk.Text);
                    cmd.Parameters.AddWithValue("@harga", int.Parse(txtHarga.Text));
                    cmd.Parameters.AddWithValue("@stok", int.Parse(txtStok.Text));
                    cmd.Parameters.AddWithValue("@gambar_produk", picProduk.ImageLocation ?? "");
                    cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Data produk berhasil diubah!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSimpan.Text = "Simpan";
            }
            DisplayData();
        }

        private void clearField()
        {
            txtProdukId.Clear();
            cmbKategoriId.SelectedIndex = -1;
            txtNamaProduk.Clear();
            txtStok.Clear();
            txtHarga.Clear();
            picProduk.ImageLocation = null;
            txtBarcode.Clear();
            GenerateProdukId();
            btnSimpan.Text = "Simpan";
        }

        private void btnScanBarcode_Click(object sender, EventArgs e)
        {
            if (videoSource == null || !videoSource.IsRunning)
            {
                if (cb_camera.SelectedIndex >= 0)
                {
                    barcodeDetected = false;
                    videoSource = new VideoCaptureDevice(CaptureDevices[cb_camera.SelectedIndex].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                    videoSource.Start();
                    btnScanBarcode.Text = "Stop Scan";
                    if (btnScanBarcode.Text == "Stop Scan")
                    {
                        this.ControlBox = false;
                    }
                }
                else
                {
                    MessageBox.Show("Pilih kamera terlebih dahulu.");
                }
            }
            else
            {
                Task.Run(() =>
                {
                    StopCamera();
                    if (picBarcode.InvokeRequired)
                    {
                        picBarcode.Invoke(new MethodInvoker(() => picBarcode.Image = null));
                    }
                    else
                    {
                        picBarcode.Image = null;
                    }
                });
            }

        }


        private readonly object cameraLock = new object();

        private void StopCamera()
        {
            lock (cameraLock)
            {
                if (videoSource != null)
                {
                    if (videoSource.IsRunning)
                    {
                        try
                        {
                            videoSource.SignalToStop();
                            videoSource.WaitForStop();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Gagal menghentikan kamera: " + ex.Message);
                        }
                    }

                    videoSource.NewFrame -= new NewFrameEventHandler(VideoSource_NewFrame);
                    videoSource = null; // <- penting untuk mencegah akses ulang
                }

                if (btnScanBarcode.IsHandleCreated && !btnScanBarcode.IsDisposed)
                {
                    btnScanBarcode.Invoke(new MethodInvoker(() =>
                    {
                        btnScanBarcode.Text = "Scan Barcode";
                       
                            this.ControlBox = true;
                        
                    }));
                }
            }
        }







        private void dataGridProduk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridProduk.Rows.Count)
            {
                // Isi field dari data baris yang diklik
                txtProdukId.Text = dataGridProduk.Rows[e.RowIndex].Cells[0].Value?.ToString();
                cmbKategoriId.SelectedValue = dataGridProduk.Rows[e.RowIndex].Cells[1].Value?.ToString();
                txtNamaProduk.Text = dataGridProduk.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txtStok.Text = dataGridProduk.Rows[e.RowIndex].Cells[3].Value?.ToString();
                txtHarga.Text = dataGridProduk.Rows[e.RowIndex].Cells[4].Value?.ToString();
                picProduk.ImageLocation = dataGridProduk.Rows[e.RowIndex].Cells[5].Value?.ToString();
                txtBarcode.Text = dataGridProduk.Rows[e.RowIndex].Cells[6].Value?.ToString();

                btnSimpan.Text = "Update";
                btnBatal.Text = "Hapus";
            }
            if (txtProdukId.Text == "")
            {
                // Klik di area kosong / header — reset form

                txtProdukId.Clear();
                cmbKategoriId.SelectedIndex = -1;
                txtNamaProduk.Clear();
                txtStok.Clear();
                txtHarga.Clear();
                picProduk.ImageLocation = null;
                txtBarcode.Clear();
                GenerateProdukId();
                btnSimpan.Text = "Simpan";
                btnBatal.Text = "Batal";
            }
          
          
        }


        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (btnBatal.Text == "Batal")
            {
                StopCamera();
                if (videoSource != null && videoSource.IsRunning)
                {
                    try
                    {
                        // Hentikan kamera secara sinkron dengan aman
                        videoSource.SignalToStop();
                        videoSource.WaitForStop();
                        videoSource.NewFrame -= new NewFrameEventHandler(VideoSource_NewFrame);
                        videoSource = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghentikan kamera: " + ex.Message);
                    }
                }
                txtProdukId.Clear();
                cmbKategoriId.SelectedIndex = -1;
                txtNamaProduk.Clear();
                txtStok.Clear();
                txtHarga.Clear();
                picProduk.ImageLocation = null;
                txtBarcode.Clear();
                GenerateProdukId();
                btnSimpan.Text = "Simpan";
            }
            else if (btnBatal.Text == "Hapus")
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus produk ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                    {
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM tb_produk WHERE produk_id = @produk_id", connection);
                        cmd.Parameters.AddWithValue("@produk_id", txtProdukId.Text);
                        cmd.ExecuteNonQuery();
                    }
                    DisplayData();
                    MessageBox.Show("Data produk berhasil dihapus!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnBatal.Text = "Batal";
            }

        }

        private void frmProduk_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            StopCamera();

            if (btnScanBarcode.Text == "Stop Scan")
            {
                MessageBox.Show("Harap Stop Scanning Terlebih Dahulu! ", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (videoSource != null && videoSource.IsRunning)
            {
                try
                {
                    // Hentikan kamera secara sinkron dengan aman
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                    videoSource.NewFrame -= new NewFrameEventHandler(VideoSource_NewFrame);
                    videoSource = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghentikan kamera: " + ex.Message);
                }
            }
        }
}
}
