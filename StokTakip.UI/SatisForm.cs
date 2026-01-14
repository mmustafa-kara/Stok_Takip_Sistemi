using System;
using System.Collections.Generic;
using System.Linq; // Toplam hesaplamak için gerekli
using System.Windows.Forms;
using StokTakip.BLL;
using StokTakip.Entities;

namespace StokTakip.UI
{
    public partial class SatisForm : Form
    {
        // Satışı yapan personeli Ana Menüden alacağız
        Kullanici _personel;

        public SatisForm(Kullanici personel)
        {
            InitializeComponent();
            _personel = personel;
        }

        // Manager sınıflarımız
        UrunManager uManager = new UrunManager();
        MusteriManager mManager = new MusteriManager();
        SatisManager sManager = new SatisManager();

        // Sepetimiz (Geçici liste)
        List<SepetItem> sepet = new List<SepetItem>();

        private void SatisForm_Load(object sender, EventArgs e)
        {
           
            VerileriYukle();
            SepetGuncelle();
        }

        void VerileriYukle()
        {
            // 1. Müşterileri ComboBox'a doldur
            cmbMusteri.DataSource = mManager.TumMusterileriGetir();
            cmbMusteri.DisplayMember = "Name"; // Ekranda görünen isim
            cmbMusteri.ValueMember = "Id";     // Arka plandaki ID

            // 2. Ürünleri ComboBox'a doldur
            cmbUrun.DataSource = uManager.TumUrunleriGetir();
            cmbUrun.DisplayMember = "Name";
            cmbUrun.ValueMember = "Id";
        }

        private void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            // Seçilen ürünü al (ComboBox'tan bütün nesne olarak alıyoruz)
            Urun secilenUrun = (Urun)cmbUrun.SelectedItem;
            int adet = Convert.ToInt32(numAdet.Value);


            // Sepete ekle
            SepetItem item = new SepetItem
            {
                UrunId = secilenUrun.Id,
                UrunAdi = secilenUrun.Name,
                Adet = adet,
                BirimFiyat = secilenUrun.SatisFiyat,
                // ToplamTutar özelliği sınıfın içinde otomatik hesaplanıyor
            };

            sepet.Add(item);

            // Listeyi yenile
            SepetGuncelle();
        }

        void SepetGuncelle()
        {
            // Grid'i sıfırla ve yeniden bağla
            dgvSepet.DataSource = null;
            dgvSepet.DataSource = sepet;

            // Genel Toplamı Hesapla
            decimal genelToplam = sepet.Sum(x => x.ToplamTutar);
            lblGenelToplam.Text = genelToplam.ToString("C2"); // Para birimi formatı
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            try
            {
                if (sepet.Count == 0)
                {
                    MessageBox.Show("Sepet boş!");
                    return;
                }

                // 1. Satış Başlığı (Satis) oluştur
                Satis yeniSatis = new Satis();
                yeniSatis.MusteriId = Convert.ToInt32(cmbMusteri.SelectedValue);
                yeniSatis.PersonelId = _personel.Id; // Giriş yapan personel
                yeniSatis.SatisTarih = DateTime.Now;
                yeniSatis.ToplamTutar = sepet.Sum(x => x.ToplamTutar);

                // 2. Satış Detaylarını (List<SatisDetay>) oluştur
                List<SatisDetay> detaylar = new List<SatisDetay>();

                foreach (var item in sepet)
                {
                    detaylar.Add(new SatisDetay
                    {
                        UrunId = item.UrunId,
                        Adet = item.Adet,
                        Fiyat = item.BirimFiyat
                    });
                }

                // 3. Manager'a gönder (Transaction orada çalışacak)
                sManager.SatisYap(yeniSatis, detaylar);

                MessageBox.Show("Satış başarıyla tamamlandı. Stoklar güncellendi.");

                // Formu temizle
                sepet.Clear();
                SepetGuncelle();
                VerileriYukle(); // Stoklar değiştiği için ürünleri tekrar çekelim
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satış Hatası: " + ex.Message);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            sepet.Clear();
            SepetGuncelle();
        }
    }

    // YARDIMCI SINIF (Sepetteki ürünleri Grid'de göstermek için)
    public class SepetItem
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamTutar { get { return Adet * BirimFiyat; } }
    }
}