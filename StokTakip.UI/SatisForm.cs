using System;
using System.Collections.Generic;
using System.Linq; 
using System.Windows.Forms;
using StokTakip.BLL;
using StokTakip.Entities;

namespace StokTakip.UI
{
    public partial class SatisForm : Form
    {
        Kullanici _personel;

        public SatisForm(Kullanici personel)
        {
            InitializeComponent();
            _personel = personel;
        }

        UrunManager uManager = new UrunManager();
        MusteriManager mManager = new MusteriManager();
        SatisManager sManager = new SatisManager();

        List<SepetItem> sepet = new List<SepetItem>();

        private void SatisForm_Load(object sender, EventArgs e)
        {
           
            VerileriYukle();
            SepetGuncelle();
        }

        void VerileriYukle()
        {
            cmbMusteri.DataSource = mManager.TumMusterileriGetir();
            cmbMusteri.DisplayMember = "Name"; 
            cmbMusteri.ValueMember = "Id";  
            cmbUrun.DataSource = uManager.TumUrunleriGetir();
            cmbUrun.DisplayMember = "Name";
            cmbUrun.ValueMember = "Id";
        }

        private void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            Urun secilenUrun = (Urun)cmbUrun.SelectedItem;
            int adet = Convert.ToInt32(numAdet.Value);


            SepetItem item = new SepetItem
            {
                UrunId = secilenUrun.Id,
                UrunAdi = secilenUrun.Name,
                Adet = adet,
                BirimFiyat = secilenUrun.SatisFiyat, 
            };

            sepet.Add(item);

            SepetGuncelle();
        }

        void SepetGuncelle()
        {
            dgvSepet.DataSource = null;
            dgvSepet.DataSource = sepet;

            decimal genelToplam = sepet.Sum(x => x.ToplamTutar);
            lblGenelToplam.Text = genelToplam.ToString("C2"); 
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

                Satis yeniSatis = new Satis();
                yeniSatis.MusteriId = Convert.ToInt32(cmbMusteri.SelectedValue);
                yeniSatis.PersonelId = _personel.Id; 
                yeniSatis.SatisTarih = DateTime.Now;
                yeniSatis.ToplamTutar = sepet.Sum(x => x.ToplamTutar);

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

                sManager.SatisYap(yeniSatis, detaylar);

                MessageBox.Show("Satış başarıyla tamamlandı. Stoklar güncellendi.");


                sepet.Clear();
                SepetGuncelle();
                VerileriYukle(); 
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

    public class SepetItem
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamTutar { get { return Adet * BirimFiyat; } }
    }
}