using System;
using System.Windows.Forms;
using StokTakip.BLL;
using StokTakip.Entities;

namespace StokTakip.UI
{
    public partial class MusteriForm : Form
    {
        public MusteriForm()
        {
            InitializeComponent();
        }

        MusteriManager mManager = new MusteriManager();

        

        void Listele()
        {
            dgvMusteriler.DataSource = mManager.TumMusterileriGetir();
        }

        void Temizle()
        {
            txtAd.Text = "";
            txtIletisim.Text = "";
            txtAdres.Text = "";
            cmbTur.SelectedIndex = 0;
            lblId.Text = "0";
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                Musteri m = new Musteri();
                m.Name = txtAd.Text;
                m.Iletisim = txtIletisim.Text;
                m.Adres = txtAdres.Text;
                m.Type = cmbTur.SelectedItem.ToString(); 

                mManager.MusteriEkle(m);
                MessageBox.Show("Müşteri sisteme kaydedildi.");
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
                if (dgvMusteriler.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgvMusteriler.CurrentRow.Cells["Id"].Value);

                    DialogResult onay = MessageBox.Show("Bu müşteriyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (onay == DialogResult.Yes)
                    {
                        mManager.MusteriSil(id);
                        MessageBox.Show("Müşteri başarıyla silindi.");

                        Listele(); 
                        Temizle(); 
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silinecek müşteriyi tablodan seçiniz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme işlemi sırasında hata oluştu: " + ex.Message);
            }
        }


        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(lblId.Text);
                if (id > 0)
                {
                    Musteri m = new Musteri();
                    m.Id = id;
                    m.Name = txtAd.Text;
                    m.Iletisim = txtIletisim.Text;
                    m.Adres = txtAdres.Text;
                    m.Type = cmbTur.SelectedItem.ToString();

                    mManager.MusteriGuncelle(m);
                    MessageBox.Show("Müşteri bilgileri güncellendi.");
                    Listele();
                    Temizle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dgvMusteriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvMusteriler.Rows[e.RowIndex];
                lblId.Text = row.Cells["Id"].Value?.ToString();
                txtAd.Text = row.Cells["Name"].Value?.ToString();
                txtIletisim.Text = row.Cells["Iletisim"].Value?.ToString();
                txtAdres.Text = row.Cells["Adres"].Value?.ToString();
                string tur = row.Cells["Type"].Value?.ToString();
                if (!string.IsNullOrEmpty(tur))
                {
                    cmbTur.SelectedItem = tur;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri aktarılırken hata: " + ex.Message);
            }
        }

        private void MusteriForm_Load_1(object sender, EventArgs e)
        {
            Listele();
            cmbTur.SelectedIndex = 0;
        }
    }
}