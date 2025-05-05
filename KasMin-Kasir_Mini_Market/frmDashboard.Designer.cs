namespace KasMin_Kasir_Mini_Market
{
    partial class frmDashboard
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
            menuStrip1 = new MenuStrip();
            profilToolStripMenuItem = new ToolStripMenuItem();
            userToolStripMenuItem = new ToolStripMenuItem();
            kategoriToolStripMenuItem1 = new ToolStripMenuItem();
            produkToolStripMenuItem1 = new ToolStripMenuItem();
            logOutToolStripMenuItem = new ToolStripMenuItem();
            transaksiToolStripMenuItem = new ToolStripMenuItem();
            kasirToolStripMenuItem = new ToolStripMenuItem();
            laporanPenjualanToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { profilToolStripMenuItem, transaksiToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // profilToolStripMenuItem
            // 
            profilToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { userToolStripMenuItem, kategoriToolStripMenuItem1, produkToolStripMenuItem1, logOutToolStripMenuItem });
            profilToolStripMenuItem.Name = "profilToolStripMenuItem";
            profilToolStripMenuItem.Size = new Size(82, 29);
            profilToolStripMenuItem.Text = "Master";
            // 
            // userToolStripMenuItem
            // 
            userToolStripMenuItem.Name = "userToolStripMenuItem";
            userToolStripMenuItem.Size = new Size(270, 34);
            userToolStripMenuItem.Text = "User";
            // 
            // kategoriToolStripMenuItem1
            // 
            kategoriToolStripMenuItem1.Name = "kategoriToolStripMenuItem1";
            kategoriToolStripMenuItem1.Size = new Size(270, 34);
            kategoriToolStripMenuItem1.Text = "Kategori";
            // 
            // produkToolStripMenuItem1
            // 
            produkToolStripMenuItem1.Name = "produkToolStripMenuItem1";
            produkToolStripMenuItem1.Size = new Size(270, 34);
            produkToolStripMenuItem1.Text = "Produk";
            // 
            // logOutToolStripMenuItem
            // 
            logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            logOutToolStripMenuItem.Size = new Size(270, 34);
            logOutToolStripMenuItem.Text = "LogOut";
            // 
            // transaksiToolStripMenuItem
            // 
            transaksiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kasirToolStripMenuItem, laporanPenjualanToolStripMenuItem });
            transaksiToolStripMenuItem.Name = "transaksiToolStripMenuItem";
            transaksiToolStripMenuItem.Size = new Size(65, 29);
            transaksiToolStripMenuItem.Text = "Kasir";
            // 
            // kasirToolStripMenuItem
            // 
            kasirToolStripMenuItem.Name = "kasirToolStripMenuItem";
            kasirToolStripMenuItem.Size = new Size(270, 34);
            kasirToolStripMenuItem.Text = "Trasaksi";
            // 
            // laporanPenjualanToolStripMenuItem
            // 
            laporanPenjualanToolStripMenuItem.Name = "laporanPenjualanToolStripMenuItem";
            laporanPenjualanToolStripMenuItem.Size = new Size(270, 34);
            laporanPenjualanToolStripMenuItem.Text = "Laporan Penjualan";
            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "frmDashboard";
            Text = "frmDashboard";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem profilToolStripMenuItem;
        private ToolStripMenuItem userToolStripMenuItem;
        private ToolStripMenuItem kategoriToolStripMenuItem1;
        private ToolStripMenuItem produkToolStripMenuItem1;
        private ToolStripMenuItem logOutToolStripMenuItem;
        private ToolStripMenuItem transaksiToolStripMenuItem;
        private ToolStripMenuItem kasirToolStripMenuItem;
        private ToolStripMenuItem laporanPenjualanToolStripMenuItem;
    }
}