using StokTakip.DAL;
using StokTakip.Entities;
using System;
using System.Collections.Generic;

namespace StokTakip.BLL
{
    public class UrunManager : IUrunService
    {
        UrunDAL uDal = new UrunDAL();

        public List<Urun> TumUrunleriGetir()
        {
            return uDal.TumUrunleriGetir();
        }

        public void UrunEkle(Urun u)
        {
            // İş Kuralı: Ürün adı boş olamaz
            if (string.IsNullOrEmpty(u.Name))
                throw new Exception("Ürün adı boş bırakılamaz!");

            // İş Kuralı: Fiyat veya stok negatif olamaz
            if (u.StokAdet <= 0)
                throw new Exception("Stok 0 veya daha küçük olamaz!");

            // İş kuralı örneği: Fiyat 0 olamaz
            if (u.SatisFiyat <= 0)
                throw new Exception("Satış fiyatı 0 veya daha küçük olamaz!");

            uDal.UrunEkle(u);
        }

        public void UrunGuncelle(Urun u)
        {
            if (string.IsNullOrEmpty(u.Name))
                throw new Exception("Ürün adı boş bırakılamaz!");

            uDal.UrunGuncelle(u);
        }

        public void UrunSil(int id)
        {
            if (id <= 0)
                throw new Exception("Geçersiz ürün ID!");

            uDal.UrunSil(id);
        }

        // Mevcut kodların altına ekle
        public int StokOgren(int id)
        {
            return uDal.StokAdediGetir(id);
        }

        public string IsimOgren(int id)
        {
            return uDal.UrunAdiGetir(id);
        }
    }
}