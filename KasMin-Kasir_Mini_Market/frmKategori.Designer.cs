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
            dataGridView1 = new DataGridView();
            btnTambah = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblKategoriId
            // 
            lblKategoriId.AutoSize = true;
            lblKategoriId.Location = new Point(84, 48);
            lblKategoriId.Name = "lblKategoriId";
            lblKategoriId.Size = new Size(64, 15);
            lblKategoriId.TabIndex = 0;
            lblKategoriId.Text = "Kategori id";
            // 
            // txtKategoriId
            // 
            txtKategoriId.Location = new Point(185, 45);
            txtKategoriId.Name = "txtKategoriId";
            txtKategoriId.Size = new Size(157, 23);
            txtKategoriId.TabIndex = 1;
            // 
            // txtNamaKategori
            // 
            txtNamaKategori.Location = new Point(185, 88);
            txtNamaKategori.Name = "txtNamaKategori";
            txtNamaKategori.Size = new Size(157, 23);
            txtNamaKategori.TabIndex = 3;
            // 
            // lblNamaKategori
            // 
            lblNamaKategori.AutoSize = true;
            lblNamaKategori.Location = new Point(84, 91);
            lblNamaKategori.Name = "lblNamaKategori";
            lblNamaKategori.Size = new Size(86, 15);
            lblNamaKategori.TabIndex = 2;
            lblNamaKategori.Text = "Nama Kategori";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(47, 138);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(464, 329);
            dataGridView1.TabIndex = 4;
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(378, 57);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(94, 39);
            btnTambah.TabIndex = 5;
            btnTambah.Text = "Tambah";
            btnTambah.UseVisualStyleBackColor = true;
            // 
            // frmKategori
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 501);
            Controls.Add(btnTambah);
            Controls.Add(dataGridView1);
            Controls.Add(txtNamaKategori);
            Controls.Add(lblNamaKategori);
            Controls.Add(txtKategoriId);
            Controls.Add(lblKategoriId);
            Name = "frmKategori";
            Text = "frmKategori";
            Load += frmKategori_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKategoriId;
        private TextBox txtKategoriId;
        private TextBox txtNamaKategori;
        private Label lblNamaKategori;
        private DataGridView dataGridView1;
        private Button btnTambah;
    }
}