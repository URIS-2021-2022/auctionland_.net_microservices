using AutoMapper;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public class LicitacijaRepository : ILicitacijaRepository
    {
        private readonly LicitacijaContext context;
        private readonly IMapper mapper;

        public static List<Licitacija> Licitacije { get; set; } = new List<Licitacija>();

        public LicitacijaRepository(LicitacijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public LicitacijaConfirmation CreateLicitacija(Licitacija licitacijaModel)
        {
            var createdEntity = context.Add(licitacijaModel);
            return mapper.Map<LicitacijaConfirmation>(createdEntity.Entity);
        }

        public void DeleteLicitacija(Guid LicitacijaId)
        {
            var licitacija = GetLicitacijaById(LicitacijaId);
            context.Remove(licitacija);
        }

        public Licitacija GetLicitacijaById(Guid LicitacijaId)
        {
            return context.Licitacije.FirstOrDefault(e => e.LicitacijaId == LicitacijaId);
        }

        public List<Licitacija> GetLicitacijas(DateTime datum = default)
        {
            return context.Licitacije.Where(e => (e.Datum.Equals(datum) || datum == default)).ToList();
        }

        public void UpdateLicitacija(Licitacija licitacijaModel)
        {
/*            var licitacija = GetLicitacijaById(licitacijaModel.LicitacijaId);

            licitacija.LicitacijaId = licitacijaModel.LicitacijaId;
            licitacija.Broj = licitacijaModel.Broj;
            licitacija.Godina = licitacijaModel.Godina;
            licitacija.Datum = licitacijaModel.Datum;
            licitacija.Ogranicenje = licitacijaModel.Ogranicenje;
            licitacija.Korak_cene = licitacijaModel.Korak_cene;
            licitacija.Lista_dokumentacije_fizicka_lica = licitacijaModel.Lista_dokumentacije_fizicka_lica;
            licitacija.Lista_dokumentacije_pravna_lica = licitacijaModel.Lista_dokumentacije_pravna_lica;
            licitacija.Rok_za_dostavljanje_prijave = licitacijaModel.Rok_za_dostavljanje_prijave;

            return new LicitacijaConfirmation()
            {
                LicitacijaId = licitacija.LicitacijaId,
                Broj = licitacija.Broj,
                Datum = licitacija.Datum
            };*/
        }


    }
}
