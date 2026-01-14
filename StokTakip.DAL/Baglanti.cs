using MySql.Data.MySqlClient;

namespace StokTakip.DAL
{
    public class Baglanti
    {
        // Okulun verdiği veritabanı bilgilerine göre burayı düzenle
        // Localhost kullanıyorsan port genellikle 3306'dır.
        public static MySqlConnection GetConnection()
        {
            string connString = "Server=172.21.54.253;Database=26_132430002;User ID=26_132430002;Password=İnif123.; "; // Şifren varsa Pwd kısmına yaz
            MySqlConnection baglanti = new MySqlConnection(connString);
            return baglanti;
        }
    }
}