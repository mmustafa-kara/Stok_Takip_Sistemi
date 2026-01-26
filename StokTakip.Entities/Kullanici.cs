using System;

namespace StokTakip.Entities
{
    public class Kullanici : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Fullname { get; set; }

        public override string BilgiGetir()
        {
            return $"{Fullname} ({Role})";
        }
    }
}