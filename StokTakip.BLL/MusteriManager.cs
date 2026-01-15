using StokTakip.DAL;
using StokTakip.Entities;
using System;
using System.Collections.Generic;

namespace StokTakip.BLL
{
    public class MusteriManager
    {
        MusteriDAL mDal = new MusteriDAL();

        SatisDAL sDal = new SatisDAL();

        public List<Musteri> TumMusterileriGetir()
        {
            return mDal.TumMusterileriGetir();
        }

        public void MusteriEkle(Musteri m)
        {
            if (string.IsNullOrEmpty(m.Name))
                throw new Exception("Müşteri adı boş olamaz!");
            if (string.IsNullOrEmpty(m.Adres))
                throw new Exception("Teslimat adresi boş bırakılamaz!");

            mDal.MusteriEkle(m);
        }

        public void MusteriSil(int id)
        {
            if (id > 0)
            {
                sDal.MusteriyeAitSatislariSil(id);
                mDal.MusteriSil(id);
            }
        }

        public void MusteriGuncelle(Musteri m)
        {
            if (string.IsNullOrEmpty(m.Name) || string.IsNullOrEmpty(m.Adres))
                throw new Exception("Ad ve Adres alanları zorunludur!");

            mDal.MusteriGuncelle(m);
        }
    }
}