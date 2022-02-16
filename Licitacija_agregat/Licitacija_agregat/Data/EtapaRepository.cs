using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public class EtapaRepository : IEtapaRepository
    {

        public static List<EtapaModel> Etape { get; set; } = new List<EtapaModel>();

        public EtapaRepository()
        {
            FillData();
        }

        private void FillData()
        {

        }
        public EtapaConfirmation CreateEtapa(EtapaModel etapaModel)
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

        public EtapaModel GetEtapaById(Guid EtapaId)
        {
            return Etape.FirstOrDefault(e => e.EtapaId == EtapaId);
        }

        public List<EtapaModel> GetEtapas(DateTime dan = default)
        {
            return (from e in Etape
                    where dan == default || e.Dan.Equals(dan)
                    select e).ToList();
        }

        public EtapaConfirmation UpdateEtapa(EtapaModel etapaModel)
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
