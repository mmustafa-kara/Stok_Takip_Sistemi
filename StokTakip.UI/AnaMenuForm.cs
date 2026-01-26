using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using StokTakip.Entities;

namespace StokTakip.UI
{
    public partial class Form1 : Form
    {
        private Kullanici _girisYapanKullanici;
        private IconButton _aktifButon; 
        private Panel _solKenarCizgisi;
        public Form1(Kullanici k)
        {
            InitializeComponent();
            _girisYapanKullanici = k;
            _solKenarCizgisi = new Panel();
            _solKenarCizgisi.Size = new Size(7, 55); 
            panelMenu.Controls.Add(_solKenarCizgisi);
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }
        private void FormGetir(Form frm)
        {
            panelDesktop.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(frm);
            panelDesktop.Tag = frm;
            frm.Show();
        }
        private void ButonAktifEt(object senderBtn, Color renk)
        {
            if (senderBtn != null)
            {
                ButonPasifEt();
                _aktifButon = (IconButton)senderBtn;
                _aktifButon.BackColor = Color.FromArgb(37, 36, 81);
                _aktifButon.ForeColor = renk;
                _aktifButon.IconColor = renk;
                _aktifButon.TextAlign = ContentAlignment.MiddleCenter;
                _aktifButon.ImageAlign = ContentAlignment.MiddleRight;
                _aktifButon.TextImageRelation = TextImageRelation.TextBeforeImage;
                _solKenarCizgisi.BackColor = renk;
                _solKenarCizgisi.Location = new Point(0, _aktifButon.Location.Y);
                _solKenarCizgisi.Visible = true;
                _solKenarCizgisi.BringToFront();
            }
        }

        private void ButonPasifEt()
        {
            if (_aktifButon != null)
            {
                _aktifButon.BackColor = Color.FromArgb(36, 55, 82);
                _aktifButon.ForeColor = Color.White;
                _aktifButon.IconColor = Color.White;
                _aktifButon.TextAlign = ContentAlignment.MiddleLeft;
                _aktifButon.ImageAlign = ContentAlignment.MiddleLeft;
                _aktifButon.TextImageRelation = TextImageRelation.ImageBeforeText;
            }
        }
        private void btnUrunler_Click(object sender, EventArgs e)
        {
            ButonAktifEt(sender, Color.FromArgb(172, 126, 241));
            FormGetir(new UrunForm());
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            ButonAktifEt(sender, Color.FromArgb(249, 118, 176));
            FormGetir(new MusteriForm());
        }

        private void btnSatis_Click(object sender, EventArgs e)
        {
            ButonAktifEt(sender, Color.FromArgb(253, 138, 114)); 
            FormGetir(new SatisForm(_girisYapanKullanici));
        }

        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            ButonAktifEt(sender, Color.FromArgb(95, 77, 221));
            FormGetir(new ReportForm());
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Buton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is IconButton btn && btn != _aktifButon)
            {
                btn.BackColor = Color.FromArgb(49, 69, 99);
            }
        }

        private void Buton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is IconButton btn && btn != _aktifButon)
            {
                btn.BackColor = Color.Transparent;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            MessageBox.Show("Giriş yapan kullanıcı: " + _girisYapanKullanici.Role);
            if (_girisYapanKullanici.Role == "satis_personeli")
            {
                btnRaporlar.Visible = false;
            }
            else if (_girisYapanKullanici.Role == "depo_gorevlisi")
            {
                btnSatis.Visible = false;
                btnMusteriler.Visible = false;
                btnRaporlar.Visible = false;
            }
        }
    }
}