using MySql.Data.MySqlClient;
using StokTakip.Entities;
using System;
using System.Data;

namespace StokTakip.DAL
{
    public class KullaniciDAL
    {
        // Kullanıcı adı ve şifre kontrolü yapan metot
        public Kullanici GirisKontrol(string kAdi, string sifre)
        {
            Kullanici bulunanKullanici = null;

            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // SQL Injection'ı önlemek için parametreli sorgu kullanıyoruz
                string query = "SELECT * FROM kullanicilar WHERE userName=@user AND password=@pass";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", kAdi);
                    cmd.Parameters.AddWithValue("@pass", sifre);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bulunanKullanici = new Kullanici
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                UserName = dr["userName"].ToString(),
                                Password = dr["password"].ToString(),
                                Role = dr["role"].ToString(),
                                Fullname = dr["fullname"].ToString()
                            };
                        }
                    }
                }
            }
            return bulunanKullanici;
        }
    }
}