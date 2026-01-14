using System;
using System.Drawing; // Renk ve Konum işlemleri için
using System.Windows.Forms;
using StokTakip.Entities;

namespace StokTakip.UI
{
    public partial class AnaMenuForm : Form
    {
        Kullanici _girisYapanKullanici;

        // Yapıcı Metot
        public AnaMenuForm(Kullanici k)
        {
            InitializeComponent();
            _girisYapanKullanici = k;
        }

        // --- SOL MENÜ İŞLEMLERİ ---
        private void FormGetir(Form frm)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        // --- YETKİLENDİRME ---
        private void RoleGoreMenuAyarla()
        {
            string rol = _girisYapanKullanici.Role;

            if (rol == "satis_personeli")
            {
                ButonGizle("btnRaporlar");
            }
            else if (rol == "depo_gorevlisi")
            {
                ButonGizle("btnSatis");
                ButonGizle("btnMusteriler");
                ButonGizle("btnRaporlar");
            }
        }

        private void ButonGizle(string butonAdi)
        {
            Control[] bulunanlar = this.Controls.Find(butonAdi, true);
            if (bulunanlar.Length > 0)
            {
                bulunanlar[0].Visible = false;
            }
        }

        // --- KULLANICI BİLGİSİ (LABEL) ---
        private void KullaniciBilgisiGoster()
        {
            Label lblBilgi = new Label();
            lblBilgi.Text = $"{_girisYapanKullanici.Fullname}\n[{_girisYapanKullanici.Role.ToUpper()}]";

            // --- GÜNCELLEME 1: RENK DEĞİŞİMİ ---
            // Eski Kod: lblBilgi.ForeColor = Color.DarkBlue;
            // Yeni Kod: Daha uyumlu, ciddi bir gri tonu veya siyah.
            lblBilgi.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblBilgi.ForeColor = Color.DarkSlateGray; // İstersen Color.Black de yapabilirsin.

            lblBilgi.TextAlign = ContentAlignment.MiddleRight;
            lblBilgi.AutoSize = true;
            lblBilgi.BackColor = Color.Transparent;

            this.Controls.Add(lblBilgi);
            lblBilgi.BringToFront();

            lblBilgi.Location = new Point(this.ClientSize.Width - lblBilgi.Width - 25, 10);
            lblBilgi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        // --- BUTON TIKLAMA OLAYLARI ---

        public void btnMusteriler_Click(object sender, EventArgs e)
        {
            MusteriForm frm = new MusteriForm();
            FormGetir(frm);
        }

        public void btnUrunler_Click(object sender, EventArgs e)
        {
            UrunForm frm = new UrunForm();
            FormGetir(frm);
        }

        public void btnSatis_Click(object sender, EventArgs e)
        {
            SatisForm frm = new SatisForm(_girisYapanKullanici);
            FormGetir(frm);
        }

        public void btnRaporlar_Click(object sender, EventArgs e)
        {
            ReportForm frm = new ReportForm();
            FormGetir(frm);
        }

        // --- GÜNCELLEME 2: ÇIKIŞ BUTONU DAVRANIŞI ---
        public void btnCikis_Click(object sender, EventArgs e)
        {
            // Eski Kod: Application.Exit(); // Komple kapatıyordu.

            // Yeni Kod: Uygulamayı yeniden başlatır. 
            // Program.cs'deki Main metodu tekrar çalışacağı için LoginForm açılır.
            Application.Restart();
        }

        private void AnaMenuForm_Load_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;
            KullaniciBilgisiGoster();
            RoleGoreMenuAyarla();
        }
    }
}