using AutoMapper;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class FizickoLiceRepository : IFizickoLiceRepository
    {
        private readonly DataBaseContext context;
        private readonly IMapper mapper;

        public FizickoLiceRepository (DataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public FizickoLiceConfirmation CreateFizickoLice(FizickoLiceModel fizickoLice)
        {
            var createdEntity = context.Add(fizickoLice);
            return mapper.Map<FizickoLiceConfirmation>(createdEntity.Entity);
        }

        public void DeleteFizickoLice(Guid FizickoLiceId)
        {
            var fizickoLice = GetFizickoLiceById(FizickoLiceId);
            context.Remove(fizickoLice);
        }

        public List<FizickoLiceModel> GetFizickaLicas(string JMBG = null)
        {
            return context.FizickaLica.Where(e => (JMBG == null || e.JMBG == JMBG)).ToList();
        }

        public FizickoLiceModel GetFizickoLiceById(Guid FizickoLiceId)
        {
            return context.FizickaLica.FirstOrDefault(e => e.FizickoLiceId == FizickoLiceId);
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
