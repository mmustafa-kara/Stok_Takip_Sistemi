using MySql.Data.MySqlClient;
using StokTakip.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace StokTakip.DAL
{
    public class RaporDAL
    {
        // 1. Kritik Stok Listesi (Stok <= MinStokUyari olanlar)
        public DataTable KritikStokGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Stok adedi, uyarı limitinden az veya eşit olanları getir
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

        // 2. Toplam Ciro (Kasaya giren toplam para)
        public decimal ToplamCiroGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Satışlar tablosundaki toplamTutar sütununu topla
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

        // 3. Toplam Kâr (Satış Fiyatı - Maliyet) * Adet
        public decimal ToplamKarGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Detay tablosu ile ürünler tablosunu birleştirip kârı hesaplıyoruz
                // Formül: (SatılanFiyat - ÜrününMaliyeti) * SatılanAdet
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
        // 4. En Çok Satılan Ürünler Raporu
        public DataTable EnCokSatanlariGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Ürün tablosu ile detay tablosunu birleştirip, isme göre grupluyoruz
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

        // 5. Aylık Satış Raporu
        public DataTable AylikSatisGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Tarihi Ay-Yıl formatına çevirip (Örn: 2025-01) grupluyoruz
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

        // 6. Müşteri Bazlı Satış Raporu
        public DataTable MusteriCiroGetir()
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Müşteriler (musteriler) tablosu ile satışları birleştiriyoruz
                // NOT: Veritabanında müşteri tablosunun adı 'musteriler' kabul edilmiştir.
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