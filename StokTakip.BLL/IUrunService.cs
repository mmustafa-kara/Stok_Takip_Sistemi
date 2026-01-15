using StokTakip.Entities;
using System.Collections.Generic;

namespace StokTakip.BLL
{
    public interface IUrunService
    {
        List<Urun> TumUrunleriGetir();
        void UrunEkle(Urun u);
        void UrunGuncelle(Urun u);
        void UrunSil(int id);
        int StokOgren(int id);
        string IsimOgren(int id);
    }
}