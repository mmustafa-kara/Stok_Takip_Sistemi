namespace StokTakip.Entities
{
    public class Musteri : BaseEntity
    {
        public string Name { get; set; }
        public string Iletisim { get; set; }
        public string Type { get; set; }
        public string Adres { get; set; }
        public override string BilgiGetir()
        {
            return $"{Name} - {Type} Müşterisi";
        }
    }
}