namespace StokTakip.Entities
{
    public class SatisDetay
    {
        public int Id { get; set; }
        public int SatisId { get; set; }
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; } // O anki satış fiyatı
    }
}