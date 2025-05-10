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
            if (barcodeDetected) return;

            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            // Salin frame untuk ditampilkan (harus Invoke karena akses ke UI dari background thread)
            picBarcode.Invoke(new MethodInvoker(delegate
            {
                picBarcode.Image?.Dispose(); // buang gambar lama biar nggak bocor memori
                picBarcode.Image = (Bitmap)frame.Clone(); // tampilkan frame ke PictureBox
            }));

            try
            {
                BarcodeDecoder decoder = new BarcodeDecoder();
                var result = decoder.Decode(frame); // pakai frame asli untuk decoding

                if (result != null)
                {
                    barcodeDetected = true;

                    txtBarcode.Invoke(new MethodInvoker(delegate ()
                    {
                        txtBarcode.Text = result.Text;
                    }));

                    StopCamera();
                }
            }
            catch
            {
                // Silent catch
            }
            finally
            {
                frame.Dispose(); // buang frame setelah selesai
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
            if (videoSource == null || !videoSource.IsRunning)
            {
                if (cb_camera.SelectedIndex >= 0)
                {
                    barcodeDetected = false;
                    videoSource = new VideoCaptureDevice(CaptureDevices[cb_camera.SelectedIndex].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                    videoSource.Start();
                    btnScanBarcode.Text = "Stop Scan";
                }
                else
                {
                    MessageBox.Show("Pilih kamera terlebih dahulu.");
                }
            }
            else
            {
                StopCamera();
                btnScanBarcode.Text = "Scan Barcode";
            }
        }


        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.NewFrame -= new NewFrameEventHandler(VideoSource_NewFrame);
            }

            btnScanBarcode.Invoke(new MethodInvoker(() =>
            {
                btnScanBarcode.Text = "Scan Barcode";
            }));
        }


        private void frmProduk_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void btnSimpan_Click_1(object sender, EventArgs e)
        {
            using
        }
    }
}
