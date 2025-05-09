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
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(830, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // profilToolStripMenuItem
            // 
            profilToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { userToolStripMenuItem, kategoriToolStripMenuItem1, produkToolStripMenuItem1, logOutToolStripMenuItem });
            profilToolStripMenuItem.Name = "profilToolStripMenuItem";
            profilToolStripMenuItem.Size = new Size(55, 22);
            profilToolStripMenuItem.Text = "&Master";
            // 
            // userToolStripMenuItem
            // 
            userToolStripMenuItem.Name = "userToolStripMenuItem";
            userToolStripMenuItem.Size = new Size(118, 22);
            userToolStripMenuItem.Text = "&User";
            userToolStripMenuItem.Click += userToolStripMenuItem_Click;
            // 
            // kategoriToolStripMenuItem1
            // 
            kategoriToolStripMenuItem1.Name = "kategoriToolStripMenuItem1";
            kategoriToolStripMenuItem1.Size = new Size(118, 22);
            kategoriToolStripMenuItem1.Text = "&Kategori";
            kategoriToolStripMenuItem1.Click += kategoriToolStripMenuItem1_Click;
            // 
            // produkToolStripMenuItem1
            // 
            produkToolStripMenuItem1.Name = "produkToolStripMenuItem1";
            produkToolStripMenuItem1.Size = new Size(118, 22);
            produkToolStripMenuItem1.Text = "&Produk";
            produkToolStripMenuItem1.Click += produkToolStripMenuItem1_Click;
            // 
            // logOutToolStripMenuItem
            // 
            logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            logOutToolStripMenuItem.Size = new Size(118, 22);
            logOutToolStripMenuItem.Text = "&LogOut";
            // 
            // transaksiToolStripMenuItem
            // 
            transaksiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kasirToolStripMenuItem, laporanPenjualanToolStripMenuItem });
            transaksiToolStripMenuItem.Name = "transaksiToolStripMenuItem";
            transaksiToolStripMenuItem.Size = new Size(44, 22);
            transaksiToolStripMenuItem.Text = "&Kasir";
            // 
            // kasirToolStripMenuItem
            // 
            kasirToolStripMenuItem.Name = "kasirToolStripMenuItem";
            kasirToolStripMenuItem.Size = new Size(180, 22);
            kasirToolStripMenuItem.Text = "&Trasaksi";
            kasirToolStripMenuItem.Click += kasirToolStripMenuItem_Click;
            // 
            // laporanPenjualanToolStripMenuItem
            // 
            laporanPenjualanToolStripMenuItem.Name = "laporanPenjualanToolStripMenuItem";
            laporanPenjualanToolStripMenuItem.Size = new Size(180, 22);
            laporanPenjualanToolStripMenuItem.Text = "&Laporan Penjualan";
            laporanPenjualanToolStripMenuItem.Click += laporanPenjualanToolStripMenuItem_Click;
            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(830, 437);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            Name = "frmDashboard";
            Text = "frmDashboard";
            WindowState = FormWindowState.Maximized;
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