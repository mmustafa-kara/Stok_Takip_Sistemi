using MySql.Data.MySqlClient;
using StokTakip.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace StokTakip.DAL
{
    public class RaporDAL
    {
        public DataTable KritikStokGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT name AS 'Ürün Adı', stokAdet AS 'Kalan Stok', minStokUyari AS 'Kritik Eşik' " +
                               "FROM urunler WHERE stokAdet <= minStokUyari";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public decimal ToplamCiroGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT SUM(toplamTutar) FROM satislar";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        return Convert.ToDecimal(result);
                    return 0;
                }
            }
        }
        public decimal ToplamKarGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = @"SELECT SUM((sd.fiyat - u.maliyet) * sd.adet) 
                                 FROM satisDetay sd 
                                 JOIN urunler u ON sd.urunId = u.id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        return Convert.ToDecimal(result);
                    return 0;
                }
            }
        }
        public DataTable EnCokSatanlariGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = @"SELECT u.name AS 'Ürün Adı', SUM(sd.adet) AS 'Toplam Satış Adedi' 
                         FROM satisDetay sd 
                         JOIN urunler u ON sd.urunId = u.id 
                         GROUP BY u.name 
                         ORDER BY SUM(sd.adet) DESC 
                         LIMIT 5";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable AylikSatisGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = @"SELECT DATE_FORMAT(satisTarih, '%Y-%m') AS 'Dönem', 
                                COUNT(*) AS 'İşlem Sayısı', 
                                SUM(toplamTutar) AS 'Toplam Ciro' 
                         FROM satislar 
                         GROUP BY DATE_FORMAT(satisTarih, '%Y-%m') 
                         ORDER BY Dönem DESC";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable MusteriCiroGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = @"SELECT m.name AS 'Müşteri', 
                                SUM(s.toplamTutar) AS 'Toplam Alışveriş' 
                         FROM satislar s 
                         JOIN musteriler m ON s.musteriId = m.id 
                         GROUP BY m.name 
                         ORDER BY SUM(s.toplamTutar) DESC";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}