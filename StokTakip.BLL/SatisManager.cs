using StokTakip.DAL;
using StokTakip.Entities;
using System;
using System.Collections.Generic;

namespace StokTakip.BLL
{
    public class SatisManager
    {
        SatisDAL sDal = new SatisDAL();
        UrunManager uManager = new UrunManager();

        public void SatisYap(Satis satis, List<SatisDetay> detaylar)
        {
            if (detaylar.Count == 0)
                throw new Exception("Sepette ürün yok, satış yapılamaz!");

            if (satis.ToplamTutar <= 0)
                throw new Exception("Toplam tutar 0 veya hatalı olamaz.");

            foreach (var item in detaylar)
            {
                int mevcutStok = uManager.StokOgren(item.UrunId);
                string urunAdi = uManager.IsimOgren(item.UrunId);
                if (mevcutStok < item.Adet)
                {
                    throw new Exception($"'{urunAdi}' adlı üründen stokta yeterli yok! \nMevcut: {mevcutStok}, İstenen: {item.Adet}");
                }
            }

            sDal.SatisYap(satis, detaylar);
        }
    }


}