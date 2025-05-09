namespace KasMin_Kasir_Mini_Market
{
    partial class frmTransaksi
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
            dataGridView1 = new DataGridView();
            txtTransaksiId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtQuantity = new TextBox();
            label8 = new Label();
            label9 = new Label();
            picBarcode = new PictureBox();
            btnBayar = new Button();
            btnHapus = new Button();
            btnUpdate = new Button();
            label6 = new Label();
            label7 = new Label();
            cmbNamaKasir = new ComboBox();
            dtpTanggal = new DateTimePicker();
            cmbNamaKategori = new ComboBox();
            cmbNamaProduk = new ComboBox();
            btnTambahProduk = new Button();
            label10 = new Label();
            label11 = new Label();
            lblTotal = new Label();
            label13 = new Label();
            lblStok = new Label();
            btnBatalkan = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBarcode).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(277, 131);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(592, 335);
            dataGridView1.TabIndex = 0;
            // 
            // txtTransaksiId
            // 
            txtTransaksiId.Location = new Point(32, 156);
            txtTransaksiId.Margin = new Padding(2);
            txtTransaksiId.Name = "txtTransaksiId";
            txtTransaksiId.Size = new Size(204, 23);
            txtTransaksiId.TabIndex = 1;
            // 
            // label1
            // 
            label1.Location = new Point(277, 106);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(592, 23);
            label1.TabIndex = 2;
            label1.Text = "Detail Trasaksi";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 139);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 3;
            label2.Text = "Trasaksi ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 188);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 4;
            label3.Text = "Nama Kasir";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 290);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(86, 15);
            label4.TabIndex = 8;
            label4.Text = "Nama Kategori";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(32, 241);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(49, 15);
            label5.TabIndex = 7;
            label5.Text = "Tanggal";
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(32, 411);
            txtQuantity.Margin = new Padding(2);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(204, 23);
            txtQuantity.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(32, 394);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(53, 15);
            label8.TabIndex = 12;
            label8.Text = "Quantity";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(32, 345);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(80, 15);
            label9.TabIndex = 11;
            label9.Text = "Nama Produk";
            // 
            // picBarcode
            // 
            picBarcode.Location = new Point(909, 173);
            picBarcode.Margin = new Padding(2);
            picBarcode.Name = "picBarcode";
            picBarcode.Size = new Size(204, 98);
            picBarcode.TabIndex = 14;
            picBarcode.TabStop = false;
            picBarcode.Click += pictureBox1_Click;
            // 
            // btnBayar
            // 
            btnBayar.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            btnBayar.Location = new Point(909, 394);
            btnBayar.Margin = new Padding(2);
            btnBayar.Name = "btnBayar";
            btnBayar.Size = new Size(204, 51);
            btnBayar.TabIndex = 15;
            btnBayar.Text = "Bayar";
            btnBayar.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            btnHapus.Location = new Point(1016, 284);
            btnHapus.Margin = new Padding(2);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(97, 30);
            btnHapus.TabIndex = 16;
            btnHapus.Text = "Hapus";
            btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(909, 284);
            btnUpdate.Margin = new Padding(2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(97, 30);
            btnUpdate.TabIndex = 17;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(981, 141);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(64, 20);
            label6.TabIndex = 18;
            label6.Text = "Barcode";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label7.Font = new Font("Arial Rounded MT Bold", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(1, 29);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(1145, 55);
            label7.TabIndex = 19;
            label7.Text = "Mini Market";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbNamaKasir
            // 
            cmbNamaKasir.FormattingEnabled = true;
            cmbNamaKasir.Location = new Point(34, 206);
            cmbNamaKasir.Name = "cmbNamaKasir";
            cmbNamaKasir.Size = new Size(202, 23);
            cmbNamaKasir.TabIndex = 20;
            // 
            // dtpTanggal
            // 
            dtpTanggal.Location = new Point(32, 259);
            dtpTanggal.Name = "dtpTanggal";
            dtpTanggal.Size = new Size(204, 23);
            dtpTanggal.TabIndex = 21;
            // 
            // cmbNamaKategori
            // 
            cmbNamaKategori.FormattingEnabled = true;
            cmbNamaKategori.Location = new Point(34, 308);
            cmbNamaKategori.Name = "cmbNamaKategori";
            cmbNamaKategori.Size = new Size(202, 23);
            cmbNamaKategori.TabIndex = 22;
            cmbNamaKategori.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // cmbNamaProduk
            // 
            cmbNamaProduk.FormattingEnabled = true;
            cmbNamaProduk.Location = new Point(34, 363);
            cmbNamaProduk.Name = "cmbNamaProduk";
            cmbNamaProduk.Size = new Size(202, 23);
            cmbNamaProduk.TabIndex = 23;
            // 
            // btnTambahProduk
            // 
            btnTambahProduk.Font = new Font("Segoe UI", 13F);
            btnTambahProduk.Location = new Point(34, 489);
            btnTambahProduk.Margin = new Padding(2);
            btnTambahProduk.Name = "btnTambahProduk";
            btnTambahProduk.Size = new Size(204, 44);
            btnTambahProduk.TabIndex = 24;
            btnTambahProduk.Text = "Tambah Produk";
            btnTambahProduk.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(272, 489);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(52, 25);
            label10.TabIndex = 25;
            label10.Text = "Total";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(328, 489);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(58, 45);
            label11.TabIndex = 26;
            label11.Text = "Rp";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(376, 489);
            lblTotal.Margin = new Padding(2, 0, 2, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(37, 45);
            lblTotal.TabIndex = 27;
            lblTotal.Text = "0";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F);
            label13.Location = new Point(34, 446);
            label13.Margin = new Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new Size(36, 19);
            label13.TabIndex = 28;
            label13.Text = "Stok";
            // 
            // lblStok
            // 
            lblStok.AutoSize = true;
            lblStok.Font = new Font("Segoe UI", 10F);
            lblStok.Location = new Point(78, 446);
            lblStok.Margin = new Padding(2, 0, 2, 0);
            lblStok.Name = "lblStok";
            lblStok.Size = new Size(15, 19);
            lblStok.TabIndex = 29;
            lblStok.Text = "-";
            // 
            // btnBatalkan
            // 
            btnBatalkan.Location = new Point(909, 324);
            btnBatalkan.Margin = new Padding(2);
            btnBatalkan.Name = "btnBatalkan";
            btnBatalkan.Size = new Size(204, 30);
            btnBatalkan.TabIndex = 30;
            btnBatalkan.Text = "Batalkan Transaksi";
            btnBatalkan.UseVisualStyleBackColor = true;
            // 
            // frmTransaksi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 558);
            Controls.Add(btnBatalkan);
            Controls.Add(lblStok);
            Controls.Add(label13);
            Controls.Add(lblTotal);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(btnTambahProduk);
            Controls.Add(cmbNamaProduk);
            Controls.Add(cmbNamaKategori);
            Controls.Add(dtpTanggal);
            Controls.Add(cmbNamaKasir);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(btnUpdate);
            Controls.Add(btnHapus);
            Controls.Add(btnBayar);
            Controls.Add(picBarcode);
            Controls.Add(txtQuantity);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTransaksiId);
            Controls.Add(dataGridView1);
            Margin = new Padding(2);
            Name = "frmTransaksi";
            Text = "frmTransaksi";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBarcode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox txtTransaksiId;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtQuantity;
        private Label label8;
        private Label label9;
        private PictureBox picBarcode;
        private Button btnBayar;
        private Button btnHapus;
        private Button btnUpdate;
        private Label label6;
        private Label label7;
        private ComboBox cmbNamaKasir;
        private DateTimePicker dtpTanggal;
        private ComboBox cmbNamaKategori;
        private ComboBox cmbNamaProduk;
        private Button btnTambahProduk;
        private Label label10;
        private Label label11;
        private Label lblTotal;
        private Label label13;
        private Label lblStok;
        private Button btnBatalkan;
    }
}