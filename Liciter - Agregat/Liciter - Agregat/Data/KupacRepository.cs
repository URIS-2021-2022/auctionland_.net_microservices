using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class KupacRepository : IKupacRepository
    {
        public static List<KupacModel> kupci { get; set; } = new List<KupacModel>();

        public KupacRepository()
        {
            DataFill();
        }

        private void DataFill()
        {

        }

        public KupacConfirmation CreateKupac(KupacModel kupac)
        {
            kupac.KupacId = Guid.NewGuid();
            kupci.Add(kupac);
            KupacModel kupac2 = GetKupacById(kupac.KupacId);

            if (kupac.PravnoLice == null)
            {
                return new KupacConfirmation
                {
                    KupacId = kupac2.KupacId,
                    Ime_Naziv = kupac2.FizickoLice.Ime + " " + kupac2.FizickoLice.Prezime
                };
            }
            else
            {
                return new KupacConfirmation
                {
                    KupacId = kupac2.KupacId,
                    Ime_Naziv = kupac2.PravnoLice.Naziv
                };
            }
        }

        public void DeleteKupac(Guid KupacId)
        {
            kupci.Remove(kupci.FirstOrDefault(e => e.KupacId == KupacId));
        }

        public KupacModel GetKupacById(Guid KupacId)
        {
            return kupci.FirstOrDefault(e => e.KupacId == KupacId);
        }

        public List<KupacModel> GetKupci(string JMBG_MaticniBroj = null)
        {
            throw new NotImplementedException(); //kako ovo uraditi?
        }

        public KupacConfirmation UpdateKupac(KupacModel kupac)
        {
            KupacModel kupac2 = GetKupacById(kupac.KupacId);

            kupac2.KupacId = kupac.KupacId;
            kupac2.ImaZabranu = kupac.ImaZabranu;
            kupac2.DatumPocetkaZabrane = kupac.DatumPocetkaZabrane;
            kupac2.DatumPrestankaZabrane = kupac.DatumPrestankaZabrane;
            kupac2.DuzinaTrajanjaZabraneGod = kupac.DuzinaTrajanjaZabraneGod;
            kupac2.FizickoLice = kupac.FizickoLice;
            kupac2.JavnaNadmetanja = kupac.JavnaNadmetanja;
            kupac2.OstvarenaPovrsina = kupac.OstvarenaPovrsina;
            kupac2.OvlascenoLice = kupac.OvlascenoLice;
            kupac2.PravnoLice = kupac.PravnoLice;
            kupac2.Prioritet = kupac.Prioritet;
            kupac2.Uplate = kupac.Uplate;

            if (kupac2.PravnoLice == null)
            {
                return new KupacConfirmation
                {
                    KupacId = kupac2.KupacId,
                    Ime_Naziv = kupac2.FizickoLice.Ime + " " + kupac2.FizickoLice.Prezime

                };
            }
            else
            {
                return new KupacConfirmation
                {
                    KupacId = kupac2.KupacId,
                    Ime_Naziv = kupac2.PravnoLice.Naziv

                };
            }
        }
    }
}
