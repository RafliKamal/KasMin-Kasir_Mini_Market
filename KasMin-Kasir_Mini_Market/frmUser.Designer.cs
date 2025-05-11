namespace KasMin_Kasir_Mini_Market
{
    partial class frmUser
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
            btnTambah = new Button();
            dataGridView1 = new DataGridView();
            txtPassword = new TextBox();
            lblNamaKategori = new Label();
            txtUserId = new TextBox();
            lblKategoriId = new Label();
            label1 = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            cmbRole = new ComboBox();
            label3 = new Label();
            btnBatal = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(116, 365);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(77, 39);
            btnTambah.TabIndex = 11;
            btnTambah.Text = "Tambah";
            btnTambah.UseVisualStyleBackColor = true;
            btnTambah.Click += btnTambah_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(235, 43);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(464, 391);
            dataGridView1.TabIndex = 10;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(36, 262);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(157, 23);
            txtPassword.TabIndex = 9;
            // 
            // lblNamaKategori
            // 
            lblNamaKategori.AutoSize = true;
            lblNamaKategori.Location = new Point(36, 244);
            lblNamaKategori.Name = "lblNamaKategori";
            lblNamaKategori.Size = new Size(57, 15);
            lblNamaKategori.TabIndex = 8;
            lblNamaKategori.Text = "Password";
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(36, 148);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(157, 23);
            txtUserId.TabIndex = 7;
            // 
            // lblKategoriId
            // 
            lblKategoriId.AutoSize = true;
            lblKategoriId.Location = new Point(36, 130);
            lblKategoriId.Name = "lblKategoriId";
            lblKategoriId.Size = new Size(43, 15);
            lblKategoriId.TabIndex = 6;
            lblKategoriId.Text = "User id";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 296);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 14;
            label1.Text = "Role";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(36, 205);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(157, 23);
            txtUsername.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 187);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 12;
            label2.Text = "Username";
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(36, 314);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(157, 23);
            cmbRole.TabIndex = 15;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(-1, 43);
            label3.Name = "label3";
            label3.Size = new Size(230, 70);
            label3.TabIndex = 16;
            label3.Text = "Data User";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // btnBatal
            // 
            btnBatal.Location = new Point(36, 365);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(77, 39);
            btnBatal.TabIndex = 17;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = true;
            btnBatal.Click += btnBatal_Click;
            // 
            // frmUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(726, 477);
            Controls.Add(btnBatal);
            Controls.Add(label3);
            Controls.Add(cmbRole);
            Controls.Add(label1);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(btnTambah);
            Controls.Add(dataGridView1);
            Controls.Add(txtPassword);
            Controls.Add(lblNamaKategori);
            Controls.Add(txtUserId);
            Controls.Add(lblKategoriId);
            Name = "frmUser";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmUser";
            Load += frmUser_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTambah;
        private DataGridView dataGridView1;
        private TextBox txtPassword;
        private Label lblNamaKategori;
        private TextBox txtUserId;
        private Label lblKategoriId;
        private Label label1;
        private TextBox txtUsername;
        private Label label2;
        private ComboBox cmbRole;
        private Label label3;
        private Button btnBatal;
    }
}