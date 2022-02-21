using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;
using Program_Agregat.Entities;

namespace Program_Agregat.Data
{

    public class PredlogPlanaRepository : IPredlogPlanaRepository
    {
        public static List<PredlogPlana> PredloziPlana { get; set; } = new List<PredlogPlana>();

        public PredlogPlanaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            
        }

        public List<PredlogPlana> GetPredloziPlana(string BrojDokumenta = null)
        {

            return (from e in PredloziPlana
                    where string.IsNullOrEmpty(BrojDokumenta) || e.BrojDokumenta == BrojDokumenta                        
                    select e).ToList();
        }

        public PredlogPlana GetPredlogPlanaById(Guid PredlogPlanaId)
        {

            return PredloziPlana.FirstOrDefault(e => e.PredlogPlanaId == PredlogPlanaId);

        }

        public PredlogPlanaConfirmation CreatePredlogPlana(PredlogPlana predlogPlana)
        {
            predlogPlana.PredlogPlanaId = Guid.NewGuid();
            PredloziPlana.Add(predlogPlana);
            PredlogPlana predlog = GetPredlogPlanaById(predlogPlana.PredlogPlanaId);

            return new PredlogPlanaConfirmation
            {
                PredlogPlanaId = predlog.PredlogPlanaId,

                BrojDokumenta = predlog.BrojDokumenta

            };
        }

        public PredlogPlanaConfirmation UpdatePredlogPlana(PredlogPlana predlogPlana)
        {
            PredlogPlana predlog = GetPredlogPlanaById(predlogPlana.PredlogPlanaId);

            predlog.PredlogPlanaId = predlogPlana.PredlogPlanaId;
            predlog.BrojDokumenta = predlogPlana.BrojDokumenta;
            predlog.MisljenjeKomisije = predlogPlana.MIsljenjeKomisije;
            predlog.Usvojen = predlogPlana.Usvojen;
            predlog.DatumDokumenta = predlogPlana.DatumDokumenta;

            return new PredlogPlanaConfirmation
            {
                PredlogPlanaId = predlog.PredlogPlanaId,
                BrojDokumenta = predlog.BrojDokumenta

            };
        }

        public void DeletePredlogPlana(Guid PredlogPlanaId)
        {
            PredloziPlana.Remove(PredloziPlana.FirstOrDefault(e => e.PredlogPlanaId == PredlogPlanaId));
        }

    }
}