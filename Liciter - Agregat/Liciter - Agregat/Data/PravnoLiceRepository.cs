using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class PravnoLiceRepository : IPravnoLiceRepository
    {
        public static List<PravnoLiceModel> pravnaLica { get; set; } = new List<PravnoLiceModel>();

        public PravnoLiceRepository ()
        {
            FillData();
        }

        private void FillData()
        {

        }
        public PravnoLiceConfirmation CreatePravnoLice(PravnoLiceModel pravnoLice)
        {
            pravnoLice.PravnoLiceId = Guid.NewGuid();
            pravnaLica.Add(pravnoLice);
            PravnoLiceModel lice = GetPravnoLiceById(pravnoLice.PravnoLiceId);

            return new PravnoLiceConfirmation
            {
                PravnoLiceId = lice.PravnoLiceId,

                Naziv = lice.Naziv,

                MaticniBroj = lice.MaticniBroj

            };
        }

        public void DeletePravnoLice(Guid PravnoLiceId)
        {
            pravnaLica.Remove(pravnaLica.FirstOrDefault(e => e.PravnoLiceId == PravnoLiceId));
        }

        public List<PravnoLiceModel> GetPravnaLicas(string MaticniBroj = null)
        {
            return (from e in pravnaLica
                    where string.IsNullOrEmpty(MaticniBroj) || e.MaticniBroj == MaticniBroj
                    select e).ToList();
        }

        public PravnoLiceModel GetPravnoLiceById(Guid PravnoLiceId)
        {
            return pravnaLica.FirstOrDefault(e => e.PravnoLiceId == PravnoLiceId);
        }

        public PravnoLiceConfirmation UpdatePravnoLice(PravnoLiceModel pravnoLice)
        {
            PravnoLiceModel lice = GetPravnoLiceById(pravnoLice.PravnoLiceId);

            lice.PravnoLiceId = pravnoLice.PravnoLiceId;
            lice.Adresa = pravnoLice.Adresa;
            lice.BrojRacuna = pravnoLice.BrojRacuna;
            lice.BrojTelefona1 = pravnoLice.BrojTelefona1;
            lice.BrojTelefona2 = pravnoLice.BrojTelefona2;
            lice.Email = pravnoLice.Email;
            lice.Faks = pravnoLice.Faks;
            lice.KontaktOsoba = pravnoLice.KontaktOsoba;
            lice.MaticniBroj = pravnoLice.MaticniBroj;
            lice.Naziv = pravnoLice.Naziv;

            return new PravnoLiceConfirmation
            {
                PravnoLiceId = lice.PravnoLiceId,
                Naziv = lice.Naziv,
                MaticniBroj = lice.MaticniBroj
            };
        }
    }
}
