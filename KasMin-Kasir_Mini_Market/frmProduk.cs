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
            if (barcodeDetected) return;

            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
            picBarcode.Image = (Bitmap)frame.Clone();

            try
            {
                BarcodeReader reader = new BarcodeReader();
                // Convert the Bitmap to a byte array
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    frame.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] frameBytes = memoryStream.ToArray();

                    var result = reader.Decode(frameBytes); // Pass the byte array to Decode
                    if (result != null)
                    {
                        barcodeDetected = true;

                        txtBarcode.Invoke(new MethodInvoker(delegate ()
                        {
                            txtBarcode.Text = result.Text;
                        }));

                        // Hentikan kamera setelah barcode terbaca
                        StopCamera();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca barcode: " + ex.Message);
            }
            finally
            {
                frame.Dispose();
            }
        }

        private void frmProduk_Load(object sender, EventArgs e)
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

            cb_camera.SelectedIndex = 0;
            videoSource = new VideoCaptureDevice(CaptureDevices[cb_camera.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start();

            btnBatal.Text = "Batal";
            btnSimpan.Text = "Simpan";
            GenerateProdukId();
            LoadKategori();
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

        private void picProduk_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picProduk.ImageLocation = ofd.FileName;
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (btnSimpan.Text == "Simpan")
            {
                using (MySqlConnection connection = new MySqlConnection(Koneksi.Connect))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO tb_produk 
                        (produk_id, kategori_id, nama_produk, harga, stok, gambar_produk, barcode) 
                        VALUES 
                        (@produk_id, @kategori_id, @nama_produk, @harga, @stok, @gambar_produk, @barcode)", connection);

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
            }
            else if (btnSimpan.Text == "Ubah")
            {
                MessageBox.Show("Data produk berhasil diubah!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnScanBarcode_Click(object sender, EventArgs e)
        {
            barcodeDetected = false;
            if (videoSource != null && !videoSource.IsRunning)
            {
                videoSource.Start();
            }
        }

        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.NewFrame -= new NewFrameEventHandler(VideoSource_NewFrame);
            }
        }

        private void frmProduk_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void frmProduk_Load_1(object sender, EventArgs e)
        {
        }
    }
}
