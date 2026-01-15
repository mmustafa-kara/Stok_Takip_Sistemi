using System;
using System.Windows.Forms;
using StokTakip.BLL;
using StokTakip.Entities;

namespace StokTakip.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        KullaniciManager kManager = new KullaniciManager();

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                string kAdi = txtUserName.Text.Trim();
                string sifre = txtPassword.Text.Trim();

                Kullanici user = kManager.GirisYap(kAdi, sifre);

                if (user != null)
                {
                    AnaMenuForm anaMenu = new AnaMenuForm(user);
                    anaMenu.Show();
                    this.Hide();

                    anaMenu.FormClosed += (s, args) => Application.Exit();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}