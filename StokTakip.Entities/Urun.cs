using System;

namespace StokTakip.Entities
{
    public class Urun : BaseEntity
    {
        public string Name { get; set; }
        public int StokAdet { get; set; }
        public decimal SatisFiyat { get; set; }
        public decimal Maliyet { get; set; }
        public int MinStokUyari { get; set; }
        public string UrunAciklama { get; set; }

        public override string BilgiGetir()
        {
            return $"{Name} (Stok: {StokAdet})";
        }
    }
}