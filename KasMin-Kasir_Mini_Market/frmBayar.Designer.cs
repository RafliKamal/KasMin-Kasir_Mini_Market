namespace KasMin_Kasir_Mini_Market
{
    partial class frmBayar
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
            label10 = new Label();
            label2 = new Label();
            txtUangMasuk = new TextBox();
            cmbMetode = new ComboBox();
            label1 = new Label();
            label3 = new Label();
            txtKembalian = new TextBox();
            btnLunas = new Button();
            btnBatal = new Button();
            labelTotal = new Label();
            btnCek = new Button();
            SuspendLayout();
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(45, 36);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(52, 25);
            label10.TabIndex = 28;
            label10.Text = "Total";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(56, 156);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 32;
            label2.Text = "Uang Masuk";
            // 
            // txtUangMasuk
            // 
            txtUangMasuk.Location = new Point(56, 173);
            txtUangMasuk.Margin = new Padding(2);
            txtUangMasuk.Name = "txtUangMasuk";
            txtUangMasuk.Size = new Size(218, 23);
            txtUangMasuk.TabIndex = 31;
            // 
            // cmbMetode
            // 
            cmbMetode.FormattingEnabled = true;
            cmbMetode.Location = new Point(56, 117);
            cmbMetode.Name = "cmbMetode";
            cmbMetode.Size = new Size(306, 23);
            cmbMetode.TabIndex = 34;
            cmbMetode.SelectedIndexChanged += cmbMetode_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 99);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 33;
            label1.Text = "Metode Pembayaran";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 217);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 36;
            label3.Text = "Kembalian";
            // 
            // txtKembalian
            // 
            txtKembalian.Enabled = false;
            txtKembalian.Location = new Point(56, 234);
            txtKembalian.Margin = new Padding(2);
            txtKembalian.Name = "txtKembalian";
            txtKembalian.Size = new Size(306, 23);
            txtKembalian.TabIndex = 35;
            // 
            // btnLunas
            // 
            btnLunas.Font = new Font("Segoe UI", 12F);
            btnLunas.Location = new Point(233, 282);
            btnLunas.Name = "btnLunas";
            btnLunas.Size = new Size(129, 45);
            btnLunas.TabIndex = 37;
            btnLunas.Text = "Lunas";
            btnLunas.UseVisualStyleBackColor = true;
            btnLunas.Click += btnLunas_Click;
            // 
            // btnBatal
            // 
            btnBatal.Font = new Font("Segoe UI", 12F);
            btnBatal.Location = new Point(56, 282);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(141, 45);
            btnBatal.TabIndex = 38;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = true;
            btnBatal.Click += btnBatal_Click;
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTotal.Location = new Point(101, 36);
            labelTotal.Margin = new Padding(2, 0, 2, 0);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(0, 45);
            labelTotal.TabIndex = 39;
            // 
            // btnCek
            // 
            btnCek.Location = new Point(287, 173);
            btnCek.Name = "btnCek";
            btnCek.Size = new Size(75, 24);
            btnCek.TabIndex = 40;
            btnCek.Text = "Cek";
            btnCek.UseVisualStyleBackColor = true;
            btnCek.Click += btnCek_Click;
            // 
            // frmBayar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 367);
            Controls.Add(btnCek);
            Controls.Add(labelTotal);
            Controls.Add(btnBatal);
            Controls.Add(btnLunas);
            Controls.Add(label3);
            Controls.Add(txtKembalian);
            Controls.Add(cmbMetode);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(txtUangMasuk);
            Controls.Add(label10);
            Name = "frmBayar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmBayar";
            Load += frmBayar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTotal;
        private Label lblTotal;
        private Label label10;
        private Label label2;
        private TextBox txtTransaksiId;
        private ComboBox cmbMetode;
        private Label label1;
        private Label label3;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private TextBox txtUangMasuk;
        private TextBox txtKembalian;
        private Button btnLunas;
        private Button btnBatal;
        private Button btnCek;
    }
}