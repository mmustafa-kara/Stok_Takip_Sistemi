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
                // 1. Mali Durum (Ciro ve Kar)
                decimal ciro = rManager.CiroHesapla();
                decimal kar = rManager.KarHesapla();

                lblCiro.Text = ciro.ToString("C2"); // TL formatında yaz
                lblKar.Text = kar.ToString("C2");

                // 2. Kritik Stok Listesi (Zaten vardı)
                dgvKritik.DataSource = rManager.KritikStokListesi();

                // 3. YENİ EKLENEN RAPORLAR
                // Eğer tasarımda bu isimde gridleri oluşturduysan veriler gelecek.
                dgvEnCokSatan.DataSource = rManager.EnCokSatanlariGetir();
                dgvAylikSatis.DataSource = rManager.AylikSatisGetir();
                dgvMusteriCiro.DataSource = rManager.MusteriCiroGetir();
            }
            catch (Exception ex)
            {
                // Bir hata olursa (örneğin tasarımda grid unutulursa) program çökmesin, mesaj versin
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