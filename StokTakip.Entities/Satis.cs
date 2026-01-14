using System;

namespace StokTakip.Entities
{
    public class Satis : BaseEntity
    {
        public DateTime SatisTarih { get; set; }
        public int MusteriId { get; set; }
        public int PersonelId { get; set; } // Satışı yapan personel
        public decimal ToplamTutar { get; set; }

        // Polymorphism: Satış bilgisi istendiğinde Tarih ve Tutar dönsün
        public override string BilgiGetir()
        {
            return $"Tarih: {SatisTarih.ToShortDateString()} - Tutar: {ToplamTutar:C2}";
        }
    }
}