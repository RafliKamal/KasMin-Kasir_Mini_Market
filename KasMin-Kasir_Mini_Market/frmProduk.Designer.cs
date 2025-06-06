﻿namespace KasMin_Kasir_Mini_Market
{
    partial class frmProduk
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
            txtProdukId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtStok = new TextBox();
            label4 = new Label();
            txtNamaProduk = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtHarga = new TextBox();
            dataGridProduk = new DataGridView();
            picProduk = new PictureBox();
            picBarcode = new PictureBox();
            label7 = new Label();
            label8 = new Label();
            txtBarcode = new TextBox();
            btnScanBarcode = new Button();
            btnSimpan = new Button();
            label9 = new Label();
            btnBatal = new Button();
            cmbKategoriId = new ComboBox();
            cb_camera = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridProduk).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picProduk).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBarcode).BeginInit();
            SuspendLayout();
            // 
            // txtProdukId
            // 
            txtProdukId.Location = new Point(41, 119);
            txtProdukId.Name = "txtProdukId";
            txtProdukId.Size = new Size(204, 23);
            txtProdukId.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 101);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 1;
            label1.Text = "Produk Id";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 153);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 3;
            label2.Text = "Kategori";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 266);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 7;
            label3.Text = "Stok";
            // 
            // txtStok
            // 
            txtStok.Location = new Point(41, 284);
            txtStok.Name = "txtStok";
            txtStok.Size = new Size(204, 23);
            txtStok.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(41, 211);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 5;
            label4.Text = "Nama Produk";
            // 
            // txtNamaProduk
            // 
            txtNamaProduk.Location = new Point(41, 229);
            txtNamaProduk.Name = "txtNamaProduk";
            txtNamaProduk.Size = new Size(204, 23);
            txtNamaProduk.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(41, 370);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 11;
            label5.Text = "Gambar Produk";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(41, 321);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 9;
            label6.Text = "Harga";
            // 
            // txtHarga
            // 
            txtHarga.Location = new Point(41, 339);
            txtHarga.Name = "txtHarga";
            txtHarga.Size = new Size(204, 23);
            txtHarga.TabIndex = 8;
            // 
            // dataGridProduk
            // 
            dataGridProduk.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridProduk.Location = new Point(288, 101);
            dataGridProduk.Name = "dataGridProduk";
            dataGridProduk.Size = new Size(766, 432);
            dataGridProduk.TabIndex = 14;
            dataGridProduk.CellClick += dataGridProduk_CellClick;
            // 
            // picProduk
            // 
            picProduk.Location = new Point(43, 388);
            picProduk.Name = "picProduk";
            picProduk.Size = new Size(202, 145);
            picProduk.SizeMode = PictureBoxSizeMode.StretchImage;
            picProduk.TabIndex = 15;
            picProduk.TabStop = false;
            picProduk.Click += picProduk_Click_1;
            // 
            // picBarcode
            // 
            picBarcode.Location = new Point(1100, 147);
            picBarcode.Name = "picBarcode";
            picBarcode.Size = new Size(204, 98);
            picBarcode.SizeMode = PictureBoxSizeMode.StretchImage;
            picBarcode.TabIndex = 17;
            picBarcode.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1149, 116);
            label7.Name = "label7";
            label7.Size = new Size(95, 15);
            label7.TabIndex = 16;
            label7.Text = "Gambar Barcode";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1166, 270);
            label8.Name = "label8";
            label8.Size = new Size(69, 15);
            label8.TabIndex = 19;
            label8.Text = "No Barcode";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(1100, 296);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(204, 23);
            txtBarcode.TabIndex = 18;
            // 
            // btnScanBarcode
            // 
            btnScanBarcode.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnScanBarcode.Location = new Point(1100, 348);
            btnScanBarcode.Name = "btnScanBarcode";
            btnScanBarcode.Size = new Size(204, 45);
            btnScanBarcode.TabIndex = 20;
            btnScanBarcode.Text = "Scan Barcode";
            btnScanBarcode.UseVisualStyleBackColor = true;
            btnScanBarcode.Click += btnScanBarcode_Click;
            // 
            // btnSimpan
            // 
            btnSimpan.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSimpan.Location = new Point(1100, 490);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(204, 45);
            btnSimpan.TabIndex = 21;
            btnSimpan.Text = "Simpan";
            btnSimpan.UseVisualStyleBackColor = true;
            btnSimpan.Click += btnSimpan_Click_1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(529, 34);
            label9.Name = "label9";
            label9.Size = new Size(260, 37);
            label9.TabIndex = 22;
            label9.Text = "Form Daftar Produk";
            // 
            // btnBatal
            // 
            btnBatal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBatal.Location = new Point(1100, 403);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(204, 45);
            btnBatal.TabIndex = 23;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = true;
            btnBatal.Click += btnBatal_Click;
            // 
            // cmbKategoriId
            // 
            cmbKategoriId.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategoriId.FormattingEnabled = true;
            cmbKategoriId.Location = new Point(41, 171);
            cmbKategoriId.Name = "cmbKategoriId";
            cmbKategoriId.Size = new Size(202, 23);
            cmbKategoriId.TabIndex = 24;
            // 
            // cb_camera
            // 
            cb_camera.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_camera.FormattingEnabled = true;
            cb_camera.Location = new Point(1100, 48);
            cb_camera.Name = "cb_camera";
            cb_camera.Size = new Size(204, 23);
            cb_camera.TabIndex = 25;
            // 
            // frmProduk
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1352, 565);
            Controls.Add(cb_camera);
            Controls.Add(cmbKategoriId);
            Controls.Add(btnBatal);
            Controls.Add(label9);
            Controls.Add(btnSimpan);
            Controls.Add(btnScanBarcode);
            Controls.Add(label8);
            Controls.Add(txtBarcode);
            Controls.Add(picBarcode);
            Controls.Add(label7);
            Controls.Add(picProduk);
            Controls.Add(dataGridProduk);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(txtHarga);
            Controls.Add(label3);
            Controls.Add(txtStok);
            Controls.Add(label4);
            Controls.Add(txtNamaProduk);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtProdukId);
            Name = "frmProduk";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmProduk";
            FormClosing += frmProduk_FormClosing_1;
            Load += frmProduk_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridProduk).EndInit();
            ((System.ComponentModel.ISupportInitialize)picProduk).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBarcode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtProdukId;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtStok;
        private Label label4;
        private TextBox txtNamaProduk;
        private Label label5;
        private Label label6;
        private TextBox txtHarga;
        private DataGridView dataGridProduk;
        private PictureBox picProduk;
        private PictureBox picBarcode;
        private Label label7;
        private Label label8;
        private TextBox txtBarcode;
        private Button btnScanBarcode;
        private Button btnSimpan;
        private Label label9;
        private Button btnBatal;
        private ComboBox cmbKategoriId;
        private ComboBox cb_camera;
    }
}