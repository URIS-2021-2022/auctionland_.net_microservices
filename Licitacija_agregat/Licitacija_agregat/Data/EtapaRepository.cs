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

        public static List<Etapa> Etape { get; set; } = new List<Etapa>();

        public EtapaRepository()
        {

        }

        public EtapaConfirmation CreateEtapa(Etapa etapaModel)
        {
            etapaModel.EtapaId = Guid.NewGuid();
            Etape.Add(etapaModel);
            var etapa = GetEtapaById(etapaModel.EtapaId);

            return new EtapaConfirmation()
            {
                EtapaId = etapa.EtapaId,
                Dan = etapa.Dan
               
            };
        }

        public void DeleteEtapa(Guid EtapaId)
        {
            Etape.Remove(Etape.FirstOrDefault(e => e.EtapaId == EtapaId));
        }

        public Etapa GetEtapaById(Guid EtapaId)
        {
            return Etape.FirstOrDefault(e => e.EtapaId == EtapaId);
        }

        public List<Etapa> GetEtapas(DateTime dan = default)
        {
            return (from e in Etape
                    where dan == default || e.Dan.Equals(dan)
                    select e).ToList();
        }

        public EtapaConfirmation UpdateEtapa(Etapa etapaModel)
        {
            var etapa = GetEtapaById(etapaModel.EtapaId);

            etapa.EtapaId = etapaModel.EtapaId;
            etapa.Dan = etapaModel.Dan;
            etapa.BrojEtape = etapaModel.BrojEtape;

            return new EtapaConfirmation()
            {
                EtapaId = etapa.EtapaId,
                Dan = etapa.Dan
            };
        }
    }
}
