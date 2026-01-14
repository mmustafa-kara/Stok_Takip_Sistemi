using MySql.Data.MySqlClient;
using StokTakip.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace StokTakip.DAL
{
    public class SatisDAL
    {
        // Bu metot hem Satış başlığını, hem detayları kaydeder hem de stoğu düşer.
        public void SatisYap(Satis satis, List<SatisDetay> detaylar)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // TRANSACTION BAŞLATIYORUZ (Hata olursa her şeyi geri almak için)
                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. ADIM: Satış Başlığını (satislar) tablosuna ekle
                    string satisQuery = "INSERT INTO satislar (satisTarih, musteriId, personelId, toplamTutar) " +
                                        "VALUES (@tarih, @musteriId, @personelId, @toplamTutar); " +
                                        "SELECT LAST_INSERT_ID();"; // Eklenen satışın ID'sini hemen geri al

                    int yeniSatisId = 0;

                    using (MySqlCommand cmdSatis = new MySqlCommand(satisQuery, conn, trans))
                    {
                        cmdSatis.Parameters.AddWithValue("@tarih", DateTime.Now); // Şu anki tarih
                        cmdSatis.Parameters.AddWithValue("@musteriId", satis.MusteriId);
                        cmdSatis.Parameters.AddWithValue("@personelId", satis.PersonelId);
                        cmdSatis.Parameters.AddWithValue("@toplamTutar", satis.ToplamTutar);

                        // Sorguyu çalıştır ve yeni oluşan ID'yi al
                        yeniSatisId = Convert.ToInt32(cmdSatis.ExecuteScalar());
                    }

                    // 2. ADIM: Detayları ekle ve Stoğu düş
                    foreach (var kalem in detaylar)
                    {
                        // a) Detay Ekleme
                        string detayQuery = "INSERT INTO satisDetay (satisId, urunId, adet, fiyat) VALUES (@sId, @uId, @adt, @fyt)";
                        using (MySqlCommand cmdDetay = new MySqlCommand(detayQuery, conn, trans))
                        {
                            cmdDetay.Parameters.AddWithValue("@sId", yeniSatisId); // Az önce aldığımız ID
                            cmdDetay.Parameters.AddWithValue("@uId", kalem.UrunId);
                            cmdDetay.Parameters.AddWithValue("@adt", kalem.Adet);
                            cmdDetay.Parameters.AddWithValue("@fyt", kalem.Fiyat);
                            cmdDetay.ExecuteNonQuery();
                        }

                        // b) Stok Düşme (Trigger yerine kod ile yapıyoruz)
                        string stokQuery = "UPDATE urunler SET stokAdet = stokAdet - @satilanAdet WHERE id=@urunId";
                        using (MySqlCommand cmdStok = new MySqlCommand(stokQuery, conn, trans))
                        {
                            cmdStok.Parameters.AddWithValue("@satilanAdet", kalem.Adet);
                            cmdStok.Parameters.AddWithValue("@urunId", kalem.UrunId);
                            cmdStok.ExecuteNonQuery();
                        }
                    }

                    // Her şey sorunsuz bittiyse işlemi onayla
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    // Bir hata olduysa yapılan her şeyi geri al (Rollback)
                    trans.Rollback();
                    throw new Exception("Satış sırasında hata oluştu: " + ex.Message);
                }
            }
        }
    
    // SatisDAL.cs dosyasının içine, SatisYap metodunun altına ekle:

public void MusteriyeAitSatislariSil(int musteriId)
        {
            using (MySqlConnection conn = Baglanti.GetConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // İşlemleri Transaction (Toplu İşlem) ile yapalım ki yarım kalmasın
                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. Önce bu müşterinin satışlarının DETAYLARINI sil
                    // (Çünkü detaylar satışa, satış müşteriye bağlıdır. Tersten gitmeliyiz)
                    string detayQuery = "DELETE FROM satisDetay WHERE satisId IN (SELECT id FROM satislar WHERE musteriId=@mId)";
                    using (MySqlCommand cmd = new MySqlCommand(detayQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@mId", musteriId);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Şimdi satışın BAŞLIKLARINI (Ana kayıtlarını) sil
                    string satisQuery = "DELETE FROM satislar WHERE musteriId=@mId";
                    using (MySqlCommand cmd = new MySqlCommand(satisQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@mId", musteriId);
                        cmd.ExecuteNonQuery();
                    }

                    // Hata olmadıysa onayla
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback(); // Hata varsa işlemleri geri al
                    throw new Exception("Satış geçmişi silinirken hata oluştu: " + ex.Message);
                }
            }
        }
    } }