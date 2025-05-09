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
            lblTotal = new Label();
            label11 = new Label();
            label10 = new Label();
            label1 = new Label();
            label2 = new Label();
            DataDetailTransaksi = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataHarian).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataDetailTransaksi).BeginInit();
            SuspendLayout();
            // 
            // dataHarian
            // 
            dataHarian.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataHarian.Location = new Point(28, 69);
            dataHarian.Name = "dataHarian";
            dataHarian.Size = new Size(347, 363);
            dataHarian.TabIndex = 0;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(76, 473);
            lblTotal.Margin = new Padding(2, 0, 2, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(37, 45);
            lblTotal.TabIndex = 30;
            lblTotal.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(28, 473);
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
            label10.Location = new Point(28, 448);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(133, 25);
            label10.TabIndex = 28;
            label10.Text = "Total Transaksi";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(28, 36);
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
            label2.Location = new Point(422, 36);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(142, 25);
            label2.TabIndex = 33;
            label2.Text = "Detail Transaksi";
            // 
            // DataDetailTransaksi
            // 
            DataDetailTransaksi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataDetailTransaksi.Location = new Point(422, 69);
            DataDetailTransaksi.Name = "DataDetailTransaksi";
            DataDetailTransaksi.Size = new Size(753, 363);
            DataDetailTransaksi.TabIndex = 32;
            // 
            // Laporan_Harian
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1207, 549);
            Controls.Add(label2);
            Controls.Add(DataDetailTransaksi);
            Controls.Add(label1);
            Controls.Add(lblTotal);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(dataHarian);
            Name = "Laporan_Harian";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Laporan_Harian";
            ((System.ComponentModel.ISupportInitialize)dataHarian).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataDetailTransaksi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataHarian;
        private Label lblTotal;
        private Label label11;
        private Label label10;
        private Label label1;
        private Label label2;
        private DataGridView DataDetailTransaksi;
    }
}