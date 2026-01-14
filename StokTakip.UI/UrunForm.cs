using System;
using System.Windows.Forms;
using StokTakip.BLL;
using StokTakip.Entities;

namespace StokTakip.UI
{
    public partial class UrunForm : Form
    {
        public UrunForm()
        {
            InitializeComponent();
        }

        // BLL Katmanımızı çağırıyoruz
        UrunManager uManager = new UrunManager();

        private void UrunForm_Load(object sender, EventArgs e)
        {
        }

        // Tekrar tekrar kullanacağımız listeleme metodu
        void Listele()
        {
            // DataGridView'in veri kaynağını veritabanından gelen liste yapıyoruz
            dgvUrunler.DataSource = uManager.TumUrunleriGetir();

            // ID sütununu gizleyebilirsin (İsteğe bağlı)
            // dgvUrunler.Columns["Id"].Visible = false;
        }

        void Temizle()
        {
            txtAd.Text = "";
            txtAciklama.Text = "";
            numStok.Value = 0;
            numFiyat.Value = 0;
            numMaliyet.Value = 0;
            numMinStok.Value = 0;
            lblId.Text = "0"; // ID sıfırlanır
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                Urun yeniUrun = new Urun();
                yeniUrun.Name = txtAd.Text;
                yeniUrun.StokAdet = Convert.ToInt32(numStok.Value);
                yeniUrun.SatisFiyat = numFiyat.Value;
                yeniUrun.Maliyet = numMaliyet.Value;
                yeniUrun.MinStokUyari = Convert.ToInt32(numMinStok.Value);
                yeniUrun.UrunAciklama = txtAciklama.Text;

                // BLL katmanına gönder
                uManager.UrunEkle(yeniUrun);

                MessageBox.Show("Ürün başarıyla eklendi.");
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                // lblId içindeki değeri okuyoruz
                int id = Convert.ToInt32(lblId.Text);

                if (id > 0)
                {
                    DialogResult cevap = MessageBox.Show("Bu ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (cevap == DialogResult.Yes)
                    {
                        uManager.UrunSil(id);
                        Listele();
                        Temizle();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen listeden silinecek ürünü seçiniz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(lblId.Text);

                if (id > 0)
                {
                    Urun guncellenecekUrun = new Urun();
                    guncellenecekUrun.Id = id; // Hangi ürünü güncellediğimiz önemli
                    guncellenecekUrun.Name = txtAd.Text;
                    guncellenecekUrun.StokAdet = Convert.ToInt32(numStok.Value);
                    guncellenecekUrun.SatisFiyat = numFiyat.Value;
                    guncellenecekUrun.Maliyet = numMaliyet.Value;
                    guncellenecekUrun.MinStokUyari = Convert.ToInt32(numMinStok.Value);
                    guncellenecekUrun.UrunAciklama = txtAciklama.Text;

                    uManager.UrunGuncelle(guncellenecekUrun);

                    MessageBox.Show("Ürün güncellendi.");
                    Listele();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Lütfen listeden güncellenecek ürünü seçiniz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void dgvUrunler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Listeden bir satıra tıklandığında bilgileri kutucuklara doldur
            try
            {
                // Başlık satırına tıklanırsa hata vermesin
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvUrunler.Rows[e.RowIndex];

                    lblId.Text = row.Cells["Id"].Value.ToString();
                    txtAd.Text = row.Cells["Name"].Value.ToString();
                    numStok.Value = Convert.ToDecimal(row.Cells["StokAdet"].Value);
                    numFiyat.Value = Convert.ToDecimal(row.Cells["SatisFiyat"].Value);
                    numMaliyet.Value = Convert.ToDecimal(row.Cells["Maliyet"].Value);
                    numMinStok.Value = Convert.ToDecimal(row.Cells["MinStokUyari"].Value);
                    txtAciklama.Text = row.Cells["UrunAciklama"].Value.ToString();
                }
            }
            catch (Exception)
            {
                // Boş veya hatalı bir hücreye tıklanırsa işlem yapma
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void UrunForm_Load_1(object sender, EventArgs e)
        {
            Listele();
            
        }
    }
}