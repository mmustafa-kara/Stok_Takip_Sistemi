using System;

namespace StokTakip.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public virtual string BilgiGetir()
        {
            return $"Kayıt No: {Id}";
        }
    }
}