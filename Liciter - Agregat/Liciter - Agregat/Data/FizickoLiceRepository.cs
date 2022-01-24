using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class FizickoLiceRepository : IFizickoLiceRepository
    {
        public static List<FizickoLiceModel> fizickaLica { get; set; } = new List<FizickoLiceModel>();

        public FizickoLiceRepository ()
        {
            FillData();
        }

        private void FillData()
        {

        }

        public FizickoLiceConfirmation CreateFizickoLice(FizickoLiceModel fizickoLice)
        {
            fizickoLice.FizickoLiceId = Guid.NewGuid();
            fizickaLica.Add(fizickoLice);
            FizickoLiceModel lice = GetFizickoLiceById(fizickoLice.FizickoLiceId);

            return new FizickoLiceConfirmation
            {
                FizickoLiceId = lice.FizickoLiceId,

                Ime = lice.Ime,

                Prezime = lice.Prezime

            };
        }

        public void DeleteFizickoLice(Guid FizickoLiceId)
        {
            fizickaLica.Remove(fizickaLica.FirstOrDefault(e => e.FizickoLiceId == FizickoLiceId));
        }

        public List<FizickoLiceModel> GetFizickaLicas(string JMBG = null)
        {
           return (from e in fizickaLica
                           where string.IsNullOrEmpty(JMBG) || e.JMBG == JMBG
                           select e).ToList();
        }

        public FizickoLiceModel GetFizickoLiceById(Guid FizickoLiceId)
        {
            return fizickaLica.FirstOrDefault(e => e.FizickoLiceId == FizickoLiceId);
        }

        public FizickoLiceConfirmation UpdateFizickoLice(FizickoLiceModel fizickoLice)
        {
            FizickoLiceModel lice = GetFizickoLiceById(fizickoLice.FizickoLiceId);

            lice.FizickoLiceId = fizickoLice.FizickoLiceId;
            lice.BrojTelefona1 = fizickoLice.BrojTelefona1;
            lice.BrojTelefona2 = fizickoLice.BrojTelefona2;
            lice.Adresa = fizickoLice.Adresa;
            lice.Email = fizickoLice.Email;
            lice.Ime = fizickoLice.Ime;
            lice.Prezime = fizickoLice.Prezime;
            lice.JMBG = fizickoLice.JMBG;
            lice.BrojRacuna = fizickoLice.BrojRacuna;

            return new FizickoLiceConfirmation
            {
                FizickoLiceId = lice.FizickoLiceId,
                Ime = lice.Ime,
                Prezime = lice.Prezime
            };
        }
    }
}
