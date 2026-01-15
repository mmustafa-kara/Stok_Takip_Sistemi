using System;

namespace StokTakip.Entities
{
    public class Kullanici : BaseEntity
    {

        // Veritabanındaki "userName" sütunu
        public string UserName { get; set; }

        // Veritabanındaki "password" sütunu
        public string Password { get; set; }

        // Veritabanındaki "role" sütunu
        public string Role { get; set; }

        // Veritabanındaki "fullname" sütunu
        public string Fullname { get; set; }

        // Kullanıcı bilgisi istendiğinde Adı ve Rolü dönsün
        public override string BilgiGetir()
        {
            return $"{Fullname} ({Role})";
        }
    }
}