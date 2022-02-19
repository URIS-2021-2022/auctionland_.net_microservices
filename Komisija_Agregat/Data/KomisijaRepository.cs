using System;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Data
{

    public class KomisijaRepository : IKomisijaRepository
    {
        public static List<KomisijaModel> Komisije { get; set; } = new List<KomisijaModel>();

        public KomisijaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            
        }

        public List<KomisijaModel> GetKomisije()
        {

            return (from e in Komisije
                    select e).ToList();
        }

        public KomisijaModel GetKomisijaById(Guid KomisijaId)
        {

            return Komisije.FirstOrDefault(e => e.KomisijaId == KomisijaId);

        }

        public KomisijaConfirmation CreateKomisija(KomisijaModel komisija)
        {
            komisija.KomisijaId = Guid.NewGuid();
            Komisije.Add(komisija);
            KomisijaModel kom = GetKomisijaById(komisija.KomisijaId);

            return new KomisijaConfirmation
            {
                KomisijaId = kom.KomisijaId,
                Predsednik = kom.Predsednik
            };
        }

        public KomisijaConfirmation UpdateKomisija(KomisijaModel komisija)
        {
            KomisijaModel kom = GetKomisijaById(komisija.KomisijaId);

            kom.KomisijaId = komisija.KomisijaId;
            kom.Predsednik = komisija.Predsednik;
            kom.Clanovi = komisija.Clanovi;

            return new KomisijaConfirmation
            {
                KomisijaId = kom.KomisijaId,
                Predsednik = kom.Predsednik,              
            };
        }

        public void DeleteKomisija(Guid KomisijaId)
        {
            Komisije.Remove(Komisije.FirstOrDefault(e => e.KomisijaId == KomisijaId));
        }

    }
}
