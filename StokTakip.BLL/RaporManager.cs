using StokTakip.DAL;
using System.Data;

namespace StokTakip.BLL
{
    public class RaporManager
    {
        RaporDAL rDal = new RaporDAL();

        public DataTable KritikStokListesi()
        {
            return rDal.KritikStokGetir();
        }

        public decimal CiroHesapla()
        {
            return rDal.ToplamCiroGetir();
        }

        public decimal KarHesapla()
        {
            return rDal.ToplamKarGetir();
        }

        // DAL katmanındaki yeni rapor metodlarını çağırıyoruz
        public DataTable EnCokSatanlariGetir()
        {
            return rDal.EnCokSatanlariGetir();
        }

        public DataTable AylikSatisGetir()
        {
            return rDal.AylikSatisGetir();
        }

        public DataTable MusteriCiroGetir()
        {
            return rDal.MusteriCiroGetir();
        }
    }
}