using AutoMapper;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class PravnoLiceRepository : IPravnoLiceRepository
    {

        private readonly DataBaseContext context;
        private readonly IMapper mapper;

        public PravnoLiceRepository (DataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public PravnoLiceConfirmation CreatePravnoLice(PravnoLiceModel pravnoLice)
        {
            var createdEntity = context.Add(pravnoLice);
            return mapper.Map<PravnoLiceConfirmation>(createdEntity.Entity);
        }

        public void DeletePravnoLice(Guid PravnoLiceId)
        {
            var pravnoLice = GetPravnoLiceById(PravnoLiceId);
            context.Remove(pravnoLice);
        }

        public List<PravnoLiceModel> GetPravnaLicas(string MaticniBroj = null)
        {
            return context.PravnaLica.Where(e => (MaticniBroj == null || e.MaticniBroj == MaticniBroj)).ToList();
        }

        public PravnoLiceModel GetPravnoLiceById(Guid PravnoLiceId)
        {
            return context.PravnaLica.FirstOrDefault(e => e.PravnoLiceId == PravnoLiceId);
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
