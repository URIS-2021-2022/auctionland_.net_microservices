using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Entities;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Data
{

    public class KomisijaRepository : IKomisijaRepository
    {
        public static List<Komisija> Komisije { get; set; } = new List<Komisija>();

        public KomisijaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            
        }

        public List<Komisija> GetKomisije()
        {

            return (from e in Komisije
                    select e).ToList();
        }

        public Komisija GetKomisijaById(Guid KomisijaId)
        {

            return Komisije.FirstOrDefault(e => e.KomisijaId == KomisijaId);

        }

        public KomisijaConfirmation CreateKomisija(Komisija komisija)
        {
            komisija.KomisijaId = Guid.NewGuid();
            Komisije.Add(komisija);
            Komisija kom = GetKomisijaById(komisija.KomisijaId);

            return new KomisijaConfirmation
            {
                KomisijaId = kom.KomisijaId,
                Predsednik = kom.Predsednik
            };
        }

        public KomisijaConfirmation UpdateKomisija(Komisija komisija)
        {
            Komisija kom = GetKomisijaById(komisija.KomisijaId);

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
