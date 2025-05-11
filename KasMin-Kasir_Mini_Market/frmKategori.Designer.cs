namespace KasMin_Kasir_Mini_Market
{
    partial class frmKategori
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
            lblKategoriId = new Label();
            txtKategoriId = new TextBox();
            txtNamaKategori = new TextBox();
            lblNamaKategori = new Label();
            dataKategori = new DataGridView();
            btnTambah = new Button();
            btnBatal = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataKategori).BeginInit();
            SuspendLayout();
            // 
            // lblKategoriId
            // 
            lblKategoriId.AutoSize = true;
            lblKategoriId.Location = new Point(46, 86);
            lblKategoriId.Name = "lblKategoriId";
            lblKategoriId.Size = new Size(64, 15);
            lblKategoriId.TabIndex = 0;
            lblKategoriId.Text = "Kategori id";
            // 
            // txtKategoriId
            // 
            txtKategoriId.Location = new Point(46, 104);
            txtKategoriId.Name = "txtKategoriId";
            txtKategoriId.Size = new Size(157, 23);
            txtKategoriId.TabIndex = 1;
            // 
            // txtNamaKategori
            // 
            txtNamaKategori.Location = new Point(46, 167);
            txtNamaKategori.Name = "txtNamaKategori";
            txtNamaKategori.Size = new Size(157, 23);
            txtNamaKategori.TabIndex = 3;
            // 
            // lblNamaKategori
            // 
            lblNamaKategori.AutoSize = true;
            lblNamaKategori.Location = new Point(46, 149);
            lblNamaKategori.Name = "lblNamaKategori";
            lblNamaKategori.Size = new Size(86, 15);
            lblNamaKategori.TabIndex = 2;
            lblNamaKategori.Text = "Nama Kategori";
            // 
            // dataKategori
            // 
            dataKategori.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataKategori.Location = new Point(255, 86);
            dataKategori.Name = "dataKategori";
            dataKategori.Size = new Size(273, 157);
            dataKategori.TabIndex = 4;
            dataKategori.CellClick += dataKategori_CellClick;
            // 
            // btnTambah
            // 
            btnTambah.AutoSize = true;
            btnTambah.Location = new Point(127, 208);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(76, 35);
            btnTambah.TabIndex = 5;
            btnTambah.Text = "Tambah";
            btnTambah.UseVisualStyleBackColor = true;
            btnTambah.Click += btnTambah_Click;
            // 
            // btnBatal
            // 
            btnBatal.AutoSize = true;
            btnBatal.Location = new Point(46, 208);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(76, 35);
            btnBatal.TabIndex = 6;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = true;
            btnBatal.Click += btnBatal_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold);
            label1.Location = new Point(4, 9);
            label1.Name = "label1";
            label1.Size = new Size(576, 58);
            label1.TabIndex = 7;
            label1.Text = "Data Kategori";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmKategori
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(580, 287);
            Controls.Add(label1);
            Controls.Add(btnBatal);
            Controls.Add(btnTambah);
            Controls.Add(dataKategori);
            Controls.Add(txtNamaKategori);
            Controls.Add(lblNamaKategori);
            Controls.Add(txtKategoriId);
            Controls.Add(lblKategoriId);
            Name = "frmKategori";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmKategori";
            Load += frmKategori_Load;
            ((System.ComponentModel.ISupportInitialize)dataKategori).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKategoriId;
        private TextBox txtKategoriId;
        private TextBox txtNamaKategori;
        private Label lblNamaKategori;
        private DataGridView dataKategori;
        private Button btnTambah;
        private Button btnBatal;
        private Label label1;
    }
}