using System;

namespace StokTakip.Entities
{
    public class Urun : BaseEntity
    {
        // Veritabanındaki sütun isimleriyle birebir aynı tiplerde özellikler
        public string Name { get; set; }
        public int StokAdet { get; set; }
        public decimal SatisFiyat { get; set; }
        public decimal Maliyet { get; set; }
        public int MinStokUyari { get; set; }
        public string UrunAciklama { get; set; }

        // Polymorphism Örneği: BaseEntity'deki metodu eziyoruz
        public override string BilgiGetir()
        {
            return $"{Name} (Stok: {StokAdet})";
        }
    }
}