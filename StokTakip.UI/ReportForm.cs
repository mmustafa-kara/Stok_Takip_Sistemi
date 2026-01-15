using System;
using System.Windows.Forms;
using StokTakip.BLL;

namespace StokTakip.UI
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        RaporManager rManager = new RaporManager();

        void RaporlariGetir()
        {
            try
            {
                decimal ciro = rManager.CiroHesapla();
                decimal kar = rManager.KarHesapla();

                lblCiro.Text = ciro.ToString("C2"); 
                lblKar.Text = kar.ToString("C2");

                dgvKritik.DataSource = rManager.KritikStokListesi();
                dgvEnCokSatan.DataSource = rManager.EnCokSatanlariGetir();
                dgvAylikSatis.DataSource = rManager.AylikSatisGetir();
                dgvMusteriCiro.DataSource = rManager.MusteriCiroGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Raporlar yüklenirken hata: " + ex.Message);
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            RaporlariGetir();
        }

        private void ReportForm_Load_1(object sender, EventArgs e)
        {
           
            RaporlariGetir();
        }
    }
}