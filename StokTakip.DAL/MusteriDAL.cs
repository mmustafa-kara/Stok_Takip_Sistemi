using MySql.Data.MySqlClient;
using StokTakip.Entities;
using System.Collections.Generic;
using System.Data;
using System;

namespace StokTakip.DAL
{
    public class MusteriDAL
    {
        public List<Musteri> TumMusterileriGetir()
        {
            List<Musteri> musteriler = new List<Musteri>();
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = "SELECT * FROM musteriler";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            musteriler.Add(new Musteri
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                Iletisim = dr["iletisim"].ToString(),
                                Type = dr["type"].ToString(),
                                Adres = dr["adres"].ToString()
                            });
                        }
                    }
                }
            }
            return musteriler;
        }

        public int MusteriEkle(Musteri m)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "INSERT INTO musteriler (name, iletisim, type, adres) VALUES (@ad, @tel, @tur, @adres)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", m.Name);
                    cmd.Parameters.AddWithValue("@tel", m.Iletisim);
                    cmd.Parameters.AddWithValue("@tur", m.Type);
                    cmd.Parameters.AddWithValue("@adres", m.Adres);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int MusteriSil(int id)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "DELETE FROM musteriler WHERE id=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public int MusteriGuncelle(Musteri m)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "UPDATE musteriler SET name=@ad, iletisim=@tel, type=@tur, adres=@adres WHERE id=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", m.Name);
                    cmd.Parameters.AddWithValue("@tel", m.Iletisim);
                    cmd.Parameters.AddWithValue("@tur", m.Type);
                    cmd.Parameters.AddWithValue("@adres", m.Adres);
                    cmd.Parameters.AddWithValue("@id", m.Id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}