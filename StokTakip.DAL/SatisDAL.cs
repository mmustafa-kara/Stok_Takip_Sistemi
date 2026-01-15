using MySql.Data.MySqlClient;
using StokTakip.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace StokTakip.DAL
{
    public class SatisDAL
    {
        public void SatisYap(Satis satis, List<SatisDetay> detaylar)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string satisQuery = "INSERT INTO satislar (satisTarih, musteriId, personelId, toplamTutar) " +
                                        "VALUES (@tarih, @musteriId, @personelId, @toplamTutar); " +
                                        "SELECT LAST_INSERT_ID();";

                    int yeniSatisId = 0;

                    using (MySqlCommand cmdSatis = new MySqlCommand(satisQuery, conn, trans))
                    {
                        cmdSatis.Parameters.AddWithValue("@tarih", DateTime.Now);
                        cmdSatis.Parameters.AddWithValue("@musteriId", satis.MusteriId);
                        cmdSatis.Parameters.AddWithValue("@personelId", satis.PersonelId);
                        cmdSatis.Parameters.AddWithValue("@toplamTutar", satis.ToplamTutar);
                        yeniSatisId = Convert.ToInt32(cmdSatis.ExecuteScalar());
                    }
                    foreach (var kalem in detaylar)
                    {
                        string detayQuery = "INSERT INTO satisDetay (satisId, urunId, adet, fiyat) VALUES (@sId, @uId, @adt, @fyt)";
                        using (MySqlCommand cmdDetay = new MySqlCommand(detayQuery, conn, trans))
                        {
                            cmdDetay.Parameters.AddWithValue("@sId", yeniSatisId);
                            cmdDetay.Parameters.AddWithValue("@uId", kalem.UrunId);
                            cmdDetay.Parameters.AddWithValue("@adt", kalem.Adet);
                            cmdDetay.Parameters.AddWithValue("@fyt", kalem.Fiyat);
                            cmdDetay.ExecuteNonQuery();
                        }

                        string stokQuery = "UPDATE urunler SET stokAdet = stokAdet - @satilanAdet WHERE id=@urunId";
                        using (MySqlCommand cmdStok = new MySqlCommand(stokQuery, conn, trans))
                        {
                            cmdStok.Parameters.AddWithValue("@satilanAdet", kalem.Adet);
                            cmdStok.Parameters.AddWithValue("@urunId", kalem.UrunId);
                            cmdStok.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Satış sırasında hata oluştu: " + ex.Message);
                }
            }
        }

public void MusteriyeAitSatislariSil(int musteriId)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string detayQuery = "DELETE FROM satisDetay WHERE satisId IN (SELECT id FROM satislar WHERE musteriId=@mId)";
                    using (MySqlCommand cmd = new MySqlCommand(detayQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@mId", musteriId);
                        cmd.ExecuteNonQuery();
                    }

                    string satisQuery = "DELETE FROM satislar WHERE musteriId=@mId";
                    using (MySqlCommand cmd = new MySqlCommand(satisQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@mId", musteriId);
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Satış geçmişi silinirken hata oluştu: " + ex.Message);
                }
            }
        }
    } }