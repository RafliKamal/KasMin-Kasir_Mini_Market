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
            lblTotal = new Label();
            label11 = new Label();
            label10 = new Label();
            label2 = new Label();
            txtTransaksiId = new TextBox();
            cmbMetode = new ComboBox();
            label1 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 17F);
            lblTotal.Location = new Point(138, 36);
            lblTotal.Margin = new Padding(2, 0, 2, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(26, 31);
            lblTotal.TabIndex = 30;
            lblTotal.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 17F);
            label11.Location = new Point(101, 36);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(42, 31);
            label11.TabIndex = 29;
            label11.Text = "Rp";
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
            // txtTransaksiId
            // 
            txtTransaksiId.Location = new Point(56, 173);
            txtTransaksiId.Margin = new Padding(2);
            txtTransaksiId.Name = "txtTransaksiId";
            txtTransaksiId.Size = new Size(204, 23);
            txtTransaksiId.TabIndex = 31;
            // 
            // cmbMetode
            // 
            cmbMetode.FormattingEnabled = true;
            cmbMetode.Location = new Point(56, 117);
            cmbMetode.Name = "cmbMetode";
            cmbMetode.Size = new Size(204, 23);
            cmbMetode.TabIndex = 34;
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
            // textBox1
            // 
            textBox1.Location = new Point(56, 234);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(204, 23);
            textBox1.TabIndex = 35;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(162, 290);
            button1.Name = "button1";
            button1.Size = new Size(98, 37);
            button1.TabIndex = 37;
            button1.Text = "Lunas";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F);
            button2.Location = new Point(56, 290);
            button2.Name = "button2";
            button2.Size = new Size(98, 37);
            button2.TabIndex = 38;
            button2.Text = "Batal";
            button2.UseVisualStyleBackColor = true;
            // 
            // frmBayar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 367);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(cmbMetode);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(txtTransaksiId);
            Controls.Add(lblTotal);
            Controls.Add(label11);
            Controls.Add(label10);
            Name = "frmBayar";
            Text = "frmBayar";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTotal;
        private Label label11;
        private Label label10;
        private Label label2;
        private TextBox txtTransaksiId;
        private ComboBox cmbMetode;
        private Label label1;
        private Label label3;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
    }
}