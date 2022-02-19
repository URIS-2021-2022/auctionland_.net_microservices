using AutoMapper;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public class EtapaRepository : IEtapaRepository
    {
        private readonly LicitacijaContext context;
        private readonly IMapper mapper;

        public static List<Etapa> Etape { get; set; } = new List<Etapa>();

        public EtapaRepository(LicitacijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public EtapaConfirmation CreateEtapa(Etapa etapaModel)
        {
            var createdEntity = context.Add(etapaModel);
            return mapper.Map<EtapaConfirmation>(createdEntity.Entity);
        }

        public void DeleteEtapa(Guid EtapaId)
        {
            var etapa = GetEtapaById(EtapaId);
            context.Remove(etapa);
        }

        public Etapa GetEtapaById(Guid EtapaId)
        {
            return context.Etape.FirstOrDefault(e => e.EtapaId == EtapaId);
        }

        public List<Etapa> GetEtapas(DateTime dan = default)
        {
            
            return context.Etape.Where(e => (dan == default || e.Dan == dan)).ToList();
        }

        public void UpdateEtapa(Etapa etapaModel)
        {
/*            var etapa = GetEtapaById(etapaModel.EtapaId);

            etapa.EtapaId = etapaModel.EtapaId;
            etapa.Dan = etapaModel.Dan;
            etapa.BrojEtape = etapaModel.BrojEtape;

            return new EtapaConfirmation()
            {
                EtapaId = etapa.EtapaId,
                Dan = etapa.Dan
            };*/
        }
    }
}
