using System;

namespace StokTakip.Entities
{
    // abstract: Bu sınıftan doğrudan nesne üretilmez, sadece miras alınır.
    public abstract class BaseEntity
    {
        // Tüm tablolarda ortak olan ID özelliği
        public int Id { get; set; }

        // Polymorphism (Çok Biçimlilik) için sanal bir metot
        // Alt sınıflar bunu isterse kendine göre değiştirebilir (override).
        public virtual string BilgiGetir()
        {
            return $"Kayıt No: {Id}";
        }
    }
}