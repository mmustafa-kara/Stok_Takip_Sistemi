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
                string query = @"
            SELECT 
                u.name AS 'Ürün Adı', 
                u.stokAdet AS 'Kalan Stok',
                IFNULL(DATEDIFF(NOW(), MAX(s.satisTarih)), 999) AS 'Geçen Gün',
                CASE 
                    WHEN MAX(s.satisTarih) IS NULL THEN 'Hiç Satılmadı'
                    WHEN DATEDIFF(NOW(), MAX(s.satisTarih)) <= 7 THEN '🔥 ÇOK ACİL'
                    WHEN DATEDIFF(NOW(), MAX(s.satisTarih)) <= 30 THEN '⚠️ Dikkat'
                    ELSE '🧊 Atıl Stok'
                END AS 'Aciliyet Durumu'
            FROM urunler u 
            LEFT JOIN satisDetay sd ON u.id = sd.urunId
            LEFT JOIN satislar s ON sd.satisId = s.id 
            WHERE u.stokAdet <= u.minStokUyari
            GROUP BY u.id, u.name, u.stokAdet
            ORDER BY u.stokAdet ASC";

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
                string query = @"SELECT u.name AS 'Ürün Adı', 
                                SUM(sd.adet) AS 'Toplam Satış Adedi',
                                SUM((sd.fiyat - u.maliyet) * sd.adet) AS 'Toplam Kar'
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
                string query = @"
            SELECT 
                DATE_FORMAT(s.satisTarih, '%Y-%m') AS 'Dönem', 
                COUNT(DISTINCT s.id) AS 'İşlem Sayısı', 
                SUM(sd.fiyat * sd.adet) AS 'Toplam Ciro',
                SUM((sd.fiyat - u.maliyet) * sd.adet) AS 'Aylık Kar'
            FROM satislar s 
            JOIN satisDetay sd ON s.id = sd.satisId
            JOIN urunler u ON sd.urunId = u.id 
            GROUP BY DATE_FORMAT(s.satisTarih, '%Y-%m') 
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