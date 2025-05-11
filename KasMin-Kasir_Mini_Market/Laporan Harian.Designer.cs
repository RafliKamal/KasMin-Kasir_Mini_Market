namespace KasMin_Kasir_Mini_Market
{
    partial class Laporan_Harian
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
            dataHarian = new DataGridView();
            lblTotalBulan = new Label();
            label11 = new Label();
            label10 = new Label();
            label1 = new Label();
            label2 = new Label();
            DataDetailTransaksi = new DataGridView();
            label3 = new Label();
            cmbBulan = new ComboBox();
            lbTotalKeseluruhan = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataHarian).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataDetailTransaksi).BeginInit();
            SuspendLayout();
            // 
            // dataHarian
            // 
            dataHarian.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataHarian.Location = new Point(28, 92);
            dataHarian.Name = "dataHarian";
            dataHarian.Size = new Size(347, 363);
            dataHarian.TabIndex = 0;
            dataHarian.CellClick += dataHarian_CellClick;
            // 
            // lblTotalBulan
            // 
            lblTotalBulan.AutoSize = true;
            lblTotalBulan.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalBulan.Location = new Point(470, 494);
            lblTotalBulan.Margin = new Padding(2, 0, 2, 0);
            lblTotalBulan.Name = "lblTotalBulan";
            lblTotalBulan.Size = new Size(37, 45);
            lblTotalBulan.TabIndex = 30;
            lblTotalBulan.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(422, 494);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(58, 45);
            label11.TabIndex = 29;
            label11.Text = "Rp";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(422, 469);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(207, 25);
            label10.TabIndex = 28;
            label10.Text = "Total Transaksi Bulanan";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(28, 59);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(112, 25);
            label1.TabIndex = 31;
            label1.Text = "Data Harian";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(422, 59);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(142, 25);
            label2.TabIndex = 33;
            label2.Text = "Detail Transaksi";
            // 
            // DataDetailTransaksi
            // 
            DataDetailTransaksi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataDetailTransaksi.Location = new Point(422, 92);
            DataDetailTransaksi.Name = "DataDetailTransaksi";
            DataDetailTransaksi.Size = new Size(753, 363);
            DataDetailTransaksi.TabIndex = 32;
            DataDetailTransaksi.CellClick += DataDetailTransaksi_CellClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(211, 67);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 34;
            label3.Text = "Bulan";
            // 
            // cmbBulan
            // 
            cmbBulan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBulan.FormattingEnabled = true;
            cmbBulan.Location = new Point(254, 64);
            cmbBulan.Name = "cmbBulan";
            cmbBulan.Size = new Size(121, 23);
            cmbBulan.TabIndex = 35;
            // 
            // lbTotalKeseluruhan
            // 
            lbTotalKeseluruhan.AutoSize = true;
            lbTotalKeseluruhan.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTotalKeseluruhan.Location = new Point(76, 494);
            lbTotalKeseluruhan.Margin = new Padding(2, 0, 2, 0);
            lbTotalKeseluruhan.Name = "lbTotalKeseluruhan";
            lbTotalKeseluruhan.Size = new Size(37, 45);
            lbTotalKeseluruhan.TabIndex = 38;
            lbTotalKeseluruhan.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(28, 494);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(58, 45);
            label5.TabIndex = 37;
            label5.Text = "Rp";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(28, 469);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(243, 25);
            label6.TabIndex = 36;
            label6.Text = "Total Transaksi Keseluruhan";
            // 
            // Laporan_Harian
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1207, 571);
            Controls.Add(lbTotalKeseluruhan);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(cmbBulan);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(DataDetailTransaksi);
            Controls.Add(label1);
            Controls.Add(lblTotalBulan);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(dataHarian);
            Name = "Laporan_Harian";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Laporan_Harian";
            Load += Laporan_Harian_Load;
            ((System.ComponentModel.ISupportInitialize)dataHarian).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataDetailTransaksi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataHarian;
        private Label lblTotalBulan;
        private Label label11;
        private Label label10;
        private Label label1;
        private Label label2;
        private DataGridView DataDetailTransaksi;
        private Label label3;
        private ComboBox cmbBulan;
        private Label lbTotalKeseluruhan;
        private Label label5;
        private Label label6;
    }
}