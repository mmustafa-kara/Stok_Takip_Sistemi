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
            // Basit bir boşluk kontrolü (Business Rule örneği)
            if (string.IsNullOrEmpty(kAdi) || string.IsNullOrEmpty(sifre))
            {
                throw new Exception("Kullanıcı adı veya şifre boş bırakılamaz!");
            }

            return kDal.GirisKontrol(kAdi, sifre);
        }
    }
}