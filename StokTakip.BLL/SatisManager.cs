using StokTakip.DAL;
using StokTakip.Entities;
using System;
using System.Collections.Generic;

namespace StokTakip.BLL
{
    public class SatisManager
    {
        SatisDAL sDal = new SatisDAL();

        // Stok kontrolü için UrunManager'a ihtiyacımız var
        UrunManager uManager = new UrunManager();

        public void SatisYap(Satis satis, List<SatisDetay> detaylar)
        {
            if (detaylar.Count == 0)
                throw new Exception("Sepette ürün yok, satış yapılamaz!");

            if (satis.ToplamTutar <= 0)
                throw new Exception("Toplam tutar 0 veya hatalı olamaz.");

            // --- İŞ KURALI (BUSINESS LOGIC) BURADA BAŞLIYOR ---

            // Satılacak her ürünü tek tek kontrol et
            foreach (var item in detaylar)
            {
                // 1. Veritabanındaki güncel stoğu çek
                int mevcutStok = uManager.StokOgren(item.UrunId);
                string urunAdi = uManager.IsimOgren(item.UrunId);

                // 2. Yeterli mi bak
                if (mevcutStok < item.Adet)
                {
                    // Hata fırlat (Bu hata UI'da yakalanıp kullanıcıya gösterilecek)
                    throw new Exception($"'{urunAdi}' adlı üründen stokta yeterli yok! \nMevcut: {mevcutStok}, İstenen: {item.Adet}");
                }
            }
            // --- KONTROL BİTTİ, HER ŞEY YOLUNDAYSA SATIŞI YAP ---

            sDal.SatisYap(satis, detaylar);
        }
    }


}