using System;

namespace StokTakip.Entities
{
    public class Satis : BaseEntity
    {
        public DateTime SatisTarih { get; set; }
        public int MusteriId { get; set; }
        public int PersonelId { get; set; } 
        public decimal ToplamTutar { get; set; }

        public override string BilgiGetir()
        {
            return $"Tarih: {SatisTarih.ToShortDateString()} - Tutar: {ToplamTutar:C2}";
        }
    }
}