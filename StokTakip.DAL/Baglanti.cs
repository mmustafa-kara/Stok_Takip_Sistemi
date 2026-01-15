using MySql.Data.MySqlClient;

namespace StokTakip.DAL
{
    public class Baglanti
    {
        public static MySqlConnection GetConnection()
        {
            string connString = "Server=172.21.54.253;Database=26_132430002;User ID=26_132430002;Password=İnif123.; ";
            MySqlConnection baglanti = new MySqlConnection(connString);
            return baglanti;
        }
    }
}