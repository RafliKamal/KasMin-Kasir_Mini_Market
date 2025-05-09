namespace KasMin_Kasir_Mini_Market
{
    partial class fmUser
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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(424, 157);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(94, 39);
            btnTambah.TabIndex = 11;
            btnTambah.Text = "Tambah";
            btnTambah.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(54, 228);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(464, 329);
            dataGridView1.TabIndex = 10;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(54, 187);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(157, 23);
            txtPassword.TabIndex = 9;
            // 
            // lblNamaKategori
            // 
            lblNamaKategori.AutoSize = true;
            lblNamaKategori.Location = new Point(54, 169);
            lblNamaKategori.Name = "lblNamaKategori";
            lblNamaKategori.Size = new Size(57, 15);
            lblNamaKategori.TabIndex = 8;
            lblNamaKategori.Text = "Password";
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(54, 134);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(157, 23);
            txtUserId.TabIndex = 7;
            // 
            // lblKategoriId
            // 
            lblKategoriId.AutoSize = true;
            lblKategoriId.Location = new Point(54, 116);
            lblKategoriId.Name = "lblKategoriId";
            lblKategoriId.Size = new Size(43, 15);
            lblKategoriId.TabIndex = 6;
            lblKategoriId.Text = "User id";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(250, 169);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 14;
            label1.Text = "Role";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(250, 134);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(157, 23);
            txtUsername.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(250, 116);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 12;
            label2.Text = "Username";
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(250, 187);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(157, 23);
            cmbRole.TabIndex = 15;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(-2, 18);
            label3.Name = "label3";
            label3.Size = new Size(570, 75);
            label3.TabIndex = 16;
            label3.Text = "Data User";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fmUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(569, 584);
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
            Name = "fmUser";
            Text = "frmUser";
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
    }
}