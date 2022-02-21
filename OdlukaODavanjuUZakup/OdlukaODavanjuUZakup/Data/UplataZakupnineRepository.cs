 using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public class UplataZakupnineRepository : IUplataZakupnineRepository
    {
        public static List<UplataZakupnine> UplateZakupnine { get; set; } = new List<UplataZakupnine>();

        public UplataZakupnineRepository ()
        {
            FillData();
        }
        private void FillData()
        {

        }
        public UplataZakupnineConfirmation CreateUplataZakupnine(UplataZakupnine uplataZakupnine)
        {
            uplataZakupnine.UplataZakupnineID = Guid.NewGuid();
            UplateZakupnine.Add(uplataZakupnine); 
            UplataZakupnine zakupnina = GetUplataZakupnineById(uplataZakupnine.UplataZakupnineID);

            return new UplataZakupnineConfirmation
            {
                UplataZakupnineID = zakupnina.UplataZakupnineID,
                broj_racuna = zakupnina.broj_racuna,
                datum = zakupnina.datum
            };

        }

        public void DeleteUplataZakupnine(Guid UplataZakupnineId)
        {
            UplateZakupnine.Remove(UplateZakupnine.FirstOrDefault(e => e.UplataZakupnineID == UplataZakupnineId));
        }

        public UplataZakupnine GetUplataZakupnineById(Guid UplataZakupnineId)
        {
            return UplateZakupnine.FirstOrDefault(e => e.UplataZakupnineID == UplataZakupnineId);
        }

        public List<UplataZakupnine> GetUplateZakupnine(string broj_racuna = null)
        {
            return (from e in UplateZakupnine
                    where string.IsNullOrEmpty(broj_racuna) || e.broj_racuna == broj_racuna
                    select e).ToList();
        }

        public UplataZakupnineConfirmation UpdateUplataZakupnine(UplataZakupnine uplataZakupnine)
        {
            UplataZakupnine zakupnina = GetUplataZakupnineById(uplataZakupnine.UplataZakupnineID);

            zakupnina.UplataZakupnineID = uplataZakupnine.UplataZakupnineID;
            zakupnina.broj_racuna = uplataZakupnine.broj_racuna;
            zakupnina.datum = uplataZakupnine.datum;
            zakupnina.iznos = uplataZakupnine.iznos;
            zakupnina.poziv_na_broj = uplataZakupnine.poziv_na_broj;
            zakupnina.svrha_uplate = uplataZakupnine.svrha_uplate;
            zakupnina.javno_nadmetanje = uplataZakupnine.javno_nadmetanje;
            zakupnina.uplatilac = uplataZakupnine.uplatilac;

            return new UplataZakupnineConfirmation
            {
                UplataZakupnineID = zakupnina.UplataZakupnineID,
                broj_racuna = zakupnina.broj_racuna,
                datum = zakupnina.datum
            };
        }
    }
    

    
}
