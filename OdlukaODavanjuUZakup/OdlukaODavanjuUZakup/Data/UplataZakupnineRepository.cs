using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public class UplataZakupnineRepository : IUplataZakupnineRepository
    {
        public static List<UplataZakupnineModel> uplateZakupnine { get; set; } = new List<UplataZakupnineModel>();

        public UplataZakupnineRepository ()
        {
            FillData();
        }
        private void FillData()
        {

        }
        public UplataZakupnineConfirmation CreateUplataZakupnine(UplataZakupnineModel uplataZakupnine)
        {
            uplataZakupnine.UplataZakupnineID = Guid.NewGuid();
            uplateZakupnine.Add(uplataZakupnine);
            UplataZakupnineModel zakupnina = GetUplataZakupnineById(uplataZakupnine.UplataZakupnineID);

            return new UplataZakupnineConfirmation
            {
                UplataZakupnineID = zakupnina.UplataZakupnineID,
                broj_racuna = zakupnina.broj_racuna,
                datum = zakupnina.datum
            };

        }

        public void DeleteUplataZakupnine(Guid UplataZakupnineId)
        {
            uplateZakupnine.Remove(uplateZakupnine.FirstOrDefault(e => e.UplataZakupnineID == UplataZakupnineId));
        }

        public UplataZakupnineModel GetUplataZakupnineById(Guid UplataZakupnineId)
        {
            return uplateZakupnine.FirstOrDefault(e => e.UplataZakupnineID == UplataZakupnineId);
        }

        public List<UplataZakupnineModel> GetUplateZakupnine(string broj_racuna = null)
        {
            return (from e in uplateZakupnine
                    where string.IsNullOrEmpty(broj_racuna) || e.broj_racuna == broj_racuna
                    select e).ToList();
        }

        public UplataZakupnineConfirmation UpdateUplataZakupnine(UplataZakupnineModel uplataZakupnine)
        {
            UplataZakupnineModel zakupnina = GetUplataZakupnineById(uplataZakupnine.UplataZakupnineID);

            zakupnina.UplataZakupnineID = uplataZakupnine.UplataZakupnineID;
            zakupnina.broj_racuna = uplataZakupnine.broj_racuna;
            zakupnina.datum = uplataZakupnine.datum;
            zakupnina.iznos = uplataZakupnine.iznos;
            zakupnina.poziv_na_broj = uplataZakupnine.poziv_na_broj;
            zakupnina.svrha_uplate = uplataZakupnine.svrha_uplate;

            return new UplataZakupnineConfirmation
            {
                UplataZakupnineID = zakupnina.UplataZakupnineID,
                broj_racuna = zakupnina.broj_racuna,
                datum = zakupnina.datum
            };
        }
    }
    

    
}
