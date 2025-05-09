namespace KasMin_Kasir_Mini_Market
{
    partial class cobaBarcode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cb_camera = new ComboBox();
            pb_barcode = new PictureBox();
            txtBarcode = new TextBox();
            btnStart = new Button();
            ((System.ComponentModel.ISupportInitialize)pb_barcode).BeginInit();
            SuspendLayout();
            // 
            // cb_camera
            // 
            cb_camera.FormattingEnabled = true;
            cb_camera.Location = new Point(42, 12);
            cb_camera.Name = "cb_camera";
            cb_camera.Size = new Size(430, 23);
            cb_camera.TabIndex = 0;
            // 
            // pb_barcode
            // 
            pb_barcode.Location = new Point(42, 61);
            pb_barcode.Name = "pb_barcode";
            pb_barcode.Size = new Size(430, 312);
            pb_barcode.TabIndex = 1;
            pb_barcode.TabStop = false;
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(161, 379);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(311, 23);
            txtBarcode.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(397, 415);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // cobaBarcode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 450);
            Controls.Add(btnStart);
            Controls.Add(txtBarcode);
            Controls.Add(pb_barcode);
            Controls.Add(cb_camera);
            Name = "cobaBarcode";
            Text = "cobaBarcode";
            Load += cobaBarcode_Load;
            ((System.ComponentModel.ISupportInitialize)pb_barcode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cb_camera;
        private PictureBox pb_barcode;
        private TextBox txtBarcode;
        private Button btnStart;
    }
}