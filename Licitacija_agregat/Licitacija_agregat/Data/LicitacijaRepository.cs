using AutoMapper;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using Microsoft.EntityFrameworkCore;
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
            
            return context.Licitacije.Include(d => d.ListaEtapa).FirstOrDefault(e => e.LicitacijaId == LicitacijaId);
        }

        public List<Licitacija> GetLicitacijas(DateTime datum = default)
        {
            return context.Licitacije.Include(d => d.ListaEtapa).Where(e => (e.Datum.Equals(datum) || datum == default)).ToList();
        }

        public void UpdateLicitacija(Licitacija licitacijaModel)
        {
            // Nema potrebe za implementacijom
        }


    }
}
