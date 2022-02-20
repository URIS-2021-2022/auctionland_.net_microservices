using System;
using Program_Agregat.Models;

namespace Program_Agregat.Data
{

    public class PredlogPlanaRepository : IPredlogPlanaRepository
    {
        public static List<PredlogPlanaModel> PredloziPlana { get; set; } = new List<PredlogPlanaModel>();

        public PredlogPlanaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            
        }

        public List<PredlogPlanaModel> GetPredloziPlana(int BrojDokumenta = null)
        {

            return (from e in PredloziPlana
                    where BrojDokumenta = null || e.BrojDokumenta == BrojDokumenta                        
                    select e).ToList();
        }

        public PredlogPlanaModel GetPredlogPlanaById(Guid PredlogPlanaId)
        {

            return PredloziPlana.FirstOrDefault(e => e.PredlogPlanaId == PredlogPlanaId);

        }

        public PredlogPlanaConfirmation CreatePredlogPlana(PredlogPlanaModel predlogPlana)
        {
            predlogPlana.PredlogPlanaId = Guid.NewGuid();
            PredloziPlana.Add(predlogPlana);
            PredlogPlanaModel predlog = GetPredlogPlanaById(predlogPlana.PredlogPlanaId);

            return new PredlogPlanaConfirmation
            {
                PredlogPlanaId = predlog.PredlogPlanaId,

                BrojDokumenta = predlog.BrojDokumenta

            };
        }

        public PredlogPlanaConfirmation UpdatePredlogPlana(PredlogPlanaModel predlogPlana)
        {
            PredlogPlanaModel predlog = GetPredlogPlanaById(predlogPlana.PredlogPlanaId);

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