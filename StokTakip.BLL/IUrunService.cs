using StokTakip.Entities;
using System.Collections.Generic;

namespace StokTakip.BLL
{
    // Interface: Bir sınıfın hangi metotlara sahip olması gerektiğini söyleyen "sözleşme"dir.
    public interface IUrunService
    {
        // Standart İşlemler
        List<Urun> TumUrunleriGetir();
        void UrunEkle(Urun u);
        void UrunGuncelle(Urun u);
        void UrunSil(int id);

        // Stok Kontrolü İçin Eklediklerimiz
        int StokOgren(int id);
        string IsimOgren(int id);
    }
}