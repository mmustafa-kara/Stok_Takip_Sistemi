using StokTakip.DAL;
using StokTakip.Entities;
using System;

namespace StokTakip.BLL
{
    public class KullaniciManager
    {
        KullaniciDAL kDal = new KullaniciDAL();

        public Kullanici GirisYap(string kAdi, string sifre)
        {
            if (string.IsNullOrEmpty(kAdi) || string.IsNullOrEmpty(sifre))
            {
                throw new Exception("Kullanıcı adı veya şifre boş bırakılamaz!");
            }

            return kDal.GirisKontrol(kAdi, sifre);
        }
    }
}