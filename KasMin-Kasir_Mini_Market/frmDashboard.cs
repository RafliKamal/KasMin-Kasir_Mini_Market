using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KasMin_Kasir_Mini_Market
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUser MasterUser = new frmUser();
            MasterUser.MdiParent = this;
            MasterUser.Show();

        }

        private void kategoriToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmKategori MasterKategori = new frmKategori();
            MasterKategori.MdiParent = this;
            MasterKategori.Show();
        }

        private void produkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmProduk MasterProduk = new frmProduk();
            MasterProduk.MdiParent = this;
            MasterProduk.Show();
        }

        private void kasirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransaksi transaksi = new frmTransaksi();
            transaksi.MdiParent = this;
            transaksi.Show();
        }

        private void laporanPenjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laporan_Harian laporan = new Laporan_Harian();
            laporan.MdiParent = this;
            laporan.Show();
        }
    }
}
