using MySql.Data.MySqlClient;
using StokTakip.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace StokTakip.DAL
{
    public class UrunDAL
    {
        // 1. Tüm Ürünleri Listeleme
        public List<Urun> TumUrunleriGetir()
        {
            List<Urun> urunler = new List<Urun>();

            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = "SELECT * FROM urunler";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            urunler.Add(new Urun
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                StokAdet = Convert.ToInt32(dr["stokAdet"]),
                                SatisFiyat = Convert.ToDecimal(dr["satisFiyat"]),
                                Maliyet = Convert.ToDecimal(dr["maliyet"]),
                                MinStokUyari = Convert.ToInt32(dr["minStokUyari"]),
                                UrunAciklama = dr["urunAciklama"].ToString()
                            });
                        }
                    }
                }
            }
            return urunler;
        }

        // 2. Yeni Ürün Ekleme
        public int UrunEkle(Urun u)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = "INSERT INTO urunler (name, stokAdet, satisFiyat, maliyet, minStokUyari, urunAciklama) " +
                               "VALUES (@name, @stok, @fiyat, @maliyet, @min, @aciklama)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", u.Name);
                    cmd.Parameters.AddWithValue("@stok", u.StokAdet);
                    cmd.Parameters.AddWithValue("@fiyat", u.SatisFiyat);
                    cmd.Parameters.AddWithValue("@maliyet", u.Maliyet);
                    cmd.Parameters.AddWithValue("@min", u.MinStokUyari);
                    cmd.Parameters.AddWithValue("@aciklama", u.UrunAciklama);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // 3. Ürün Güncelleme
        public int UrunGuncelle(Urun u)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = "UPDATE urunler SET name=@name, stokAdet=@stok, satisFiyat=@fiyat, maliyet=@maliyet, " +
                               "minStokUyari=@min, urunAciklama=@aciklama WHERE id=@id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", u.Name);
                    cmd.Parameters.AddWithValue("@stok", u.StokAdet);
                    cmd.Parameters.AddWithValue("@fiyat", u.SatisFiyat);
                    cmd.Parameters.AddWithValue("@maliyet", u.Maliyet);
                    cmd.Parameters.AddWithValue("@min", u.MinStokUyari);
                    cmd.Parameters.AddWithValue("@aciklama", u.UrunAciklama);
                    cmd.Parameters.AddWithValue("@id", u.Id);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // 4. Ürün Silme
        public int UrunSil(int id)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = "DELETE FROM urunler WHERE id=@id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        


        // Stok kontrolü için stok adedini çekme
        public int StokAdediGetir(int urunId)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT stokAdet FROM urunler WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", urunId);
                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }
            }
        }

        // Hata mesajında ürünün adını göstermek için ismini çekme
        public string UrunAdiGetir(int urunId)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT name FROM urunler WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", urunId);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }
    }
}