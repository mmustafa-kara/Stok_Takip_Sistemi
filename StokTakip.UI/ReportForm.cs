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

            cmbRaporTuru.Items.Clear();

            // İLK SIRAYA BOŞ SEÇENEK EKLE
            cmbRaporTuru.Items.Add("Rapor Seçiniz...");

            cmbRaporTuru.Items.Add("Genel Bakış (Dashboard)");
            cmbRaporTuru.Items.Add("Kritik Stok Listesi");
            cmbRaporTuru.Items.Add("En Çok Satan Ürünler");
            cmbRaporTuru.Items.Add("Aylık Satış Grafiği");
            cmbRaporTuru.Items.Add("Müşteri Ciro Raporu");

            // Başlangıçta ilk sıradaki "Rapor Seçiniz..." seçili gelsin
            cmbRaporTuru.SelectedIndex = 0;
        }

        private void cmbRaporTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Önce her şeyi eski yerine (Dashboard düzenine) koyuyoruz
            PanelleriSifirla();

            if (cmbRaporTuru.SelectedItem == null) return;
            string secim = cmbRaporTuru.SelectedItem.ToString();

            // SENİN İSTEDİĞİN KISIM BURASI:
            // Eğer "Rapor Seçiniz..." seçiliyse ana tabloyu gizle ve hiçbir şey yapma
            if (secim == "Rapor Seçiniz...")
            {
                tableLayoutPanel1.Visible = false;
                return;
            }

            // Eğer Dashboard seçiliyse tabloyu göster (Zaten sıfırladık ve Visible=true yaptık)
            if (secim == "Genel Bakış (Dashboard)")
            {
                tableLayoutPanel1.Visible = true;
                return;
            }

            // Tekli seçim yapıldıysa:
            // 1. Ana tabloyu gizle
            tableLayoutPanel1.Visible = false;

            // 2. Hangi kutuyu tam ekran yapacağımızı bulalım
            GroupBox secilenGrup = null;

            if (secim == "Kritik Stok Listesi") secilenGrup = groupBox2;
            else if (secim == "En Çok Satan Ürünler") secilenGrup = groupBox3;
            else if (secim == "Aylık Satış Grafiği") secilenGrup = groupBox4;
            else if (secim == "Müşteri Ciro Raporu") secilenGrup = groupBox5;

            // 3. O kutuyu formun üzerine al ve tam ekran yap
            if (secilenGrup != null)
            {
                secilenGrup.Parent = this; // Kutuyu tablodan çıkarıp forma al
                secilenGrup.Dock = DockStyle.Fill; // Kalan boşluğu doldur
                secilenGrup.BringToFront(); // En öne getir
                secilenGrup.Visible = true;
            }
        }

        private void PanelleriSifirla()
        {
            // TableLayoutPanel'i tekrar görünür yap
            tableLayoutPanel1.Visible = true;

            // Tasarım dosyasındaki orijinal yerleşimlerine göre geri koyuyoruz:

            // Aylık Satış -> (Sütun: 0, Satır: 0)
            groupBox4.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox4, new TableLayoutPanelCellPosition(0, 0));
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Visible = true;

            // En Çok Satan -> (Sütun: 1, Satır: 0)
            groupBox3.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox3, new TableLayoutPanelCellPosition(1, 0));
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Visible = true;

            // Kritik Stok -> (Sütun: 0, Satır: 1)
            groupBox2.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox2, new TableLayoutPanelCellPosition(0, 1));
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Visible = true;

            // Müşteri Ciro -> (Sütun: 1, Satır: 1)
            groupBox5.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox5, new TableLayoutPanelCellPosition(1, 1));
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Visible = true;
        }
    }
}