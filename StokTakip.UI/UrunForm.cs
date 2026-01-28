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

        UrunManager uManager = new UrunManager();

        private void UrunForm_Load(object sender, EventArgs e)
        {
        }

        void Listele()
        {
            dgvUrunler.DataSource = uManager.TumUrunleriGetir();

        }

        void Temizle()
        {
            txtAd.Text = "";
            txtAciklama.Text = "";
            numStok.Value = 0;
            numFiyat.Value = 0;
            numMaliyet.Value = 0;
            numMinStok.Value = 0;
            lblId.Text = "0";
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
                if (lblId.Text == "0" || string.IsNullOrEmpty(lblId.Text))
                {
                    MessageBox.Show("Lütfen silinecek ürünü seçiniz.");
                    return;
                }

                int id = Convert.ToInt32(lblId.Text);
                DialogResult cevap = MessageBox.Show(
                    "Bu ürünü silerseniz, geçmişte bu ürünle yapılan SATIŞ KAYITLARI DA silinecektir.\n\nRaporlarda tutarsızlık olabilir. Yine de silmek istiyor musunuz?",
                    "Kritik Silme İşlemi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (cevap == DialogResult.Yes)
                {
                    uManager.UrunSil(id);
                    MessageBox.Show("Ürün ve geçmiş kayıtları başarıyla silindi.");
                    Listele();
                    Temizle();
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
                    guncellenecekUrun.Id = id;
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

            try
            {

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