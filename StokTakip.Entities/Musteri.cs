namespace StokTakip.Entities
{
    public class Musteri : BaseEntity
    {

        // Müşteri Adı / Firma Adı
        public string Name { get; set; }

        // Telefon Numarası
        public string Iletisim { get; set; }

        // Müşteri Türü: "Perakende" (Ev) veya "Toptan" (Otel/Bayi)
        public string Type { get; set; }

        // Teslimat Adresi (Mobilya için kritik)
        public string Adres { get; set; }

        // Polymorphism: Müşteri bilgisi istendiğinde Adı ve Türü dönsün
        public override string BilgiGetir()
        {
            return $"{Name} - {Type} Müşterisi";
        }
    }
}