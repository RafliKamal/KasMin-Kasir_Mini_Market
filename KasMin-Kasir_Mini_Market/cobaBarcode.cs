using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.Barcode;
using BasselTech_CamCapture;



namespace KasMin_Kasir_Mini_Market
{
    public partial class cobaBarcode : Form
    {
        Camera cam;
        System.Windows.Forms.Timer t;
        BackgroundWorker worker;
        Bitmap? CapImage;


        public cobaBarcode()
        {
            InitializeComponent();
        
        
            t = new System.Windows.Forms.Timer();
            cam = new Camera(pb_barcode);
            worker = new BackgroundWorker();

            worker.DoWork += Worker_DoWork;
            t.Tick += T_Tick;
            t.Interval = 1;

        }
            private void T_Tick(object sender, EventArgs e)
        {
            // Capture the image from the camera
            CapImage = cam.GetBitmap();
            if (CapImage != null && !worker.IsBusy)
                worker.RunWorkerAsync();
        }
         
        

            private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BarcodeDecoder decoder = new BarcodeDecoder();
            // Decode the barcode from the captured image
            try
            {
                string decoded_text = decoder.Decode(CapImage).Text;
                MessageBox.Show(decoded_text);
            }
            catch (Exception ex)
            {
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                cam.Start();
                t.Start();

            }
            catch (Exception ex)
            {
                cam.Stop();
                MessageBox.Show("Error starting camera: " + ex.Message);
            }
        }

        private void cobaBarcode_Load(object sender, EventArgs e)
        {


        }
    }
}
