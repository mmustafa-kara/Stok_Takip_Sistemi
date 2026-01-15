using System;

namespace StokTakip.Entities
{
    public abstract class BaseEntity
    {
        // Tüm tablolarda ortak olan ID özelliği
        public int Id { get; set; }

        public virtual string BilgiGetir()
        {
            return $"Kayıt No: {Id}";
        }
    }
}