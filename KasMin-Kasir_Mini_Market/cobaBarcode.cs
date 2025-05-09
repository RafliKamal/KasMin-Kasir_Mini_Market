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
using Emgu.CV;
using Emgu.CV.Structure;
using ZXing;
using System.Drawing;

using ZXing.Common;
using AForge.Video;



namespace KasMin_Kasir_Mini_Market
{
    public partial class cobaBarcode : Form
    {
        public cobaBarcode()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterIntoCollection;
        VideoCaptureDevice videoCaptureDevice;


        private void btnStart_Click(object sender, EventArgs e)
        {
            //videoCaptureDevice = new VideoCaptureDevice(filterIntoCollection[cb_camera.SelectedIndex].MonikerString);
            //videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            //videoCaptureDevice.Start();
        }

        private void cobaBarcode_Load(object sender, EventArgs e)
        {
            //filterIntoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //foreach (FilterInfo device in filterIntoCollection)
            //    cb_camera.Items.Add(device.Name);
            //cb_camera.SelectedIndex = 0;

        }

        //private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        //{
        //    Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
        //    BarcodeReader reader = new BarcodeReader();

        //    var result = reader.Decode(bitmap);
        //    if (result != null)
        //    {
        //        txtBarcode.Invoke(new MethodInvoker(delegate ()
        //        {
        //            txtBarcode.Text = result.Text;
        //        }));
             
        //    }pb_barcode.Image = bitmap;
        //}
    }
}
