using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public class OdlukaoDavanjuuZakupuRepository :IOdlukaoDavanjuuZakupRepository
    {
        public static List<OdlukaoDavanjuuZakup> odlukeoDavanjuuZakup { get; set; } = new List<OdlukaoDavanjuuZakup>();

        public OdlukaoDavanjuuZakupuRepository()
        {
            FillData();
        }
        private void FillData()
        {

        }
        public List<OdlukaoDavanjuuZakup> GetOdluke()
        {
            return (from e in odlukeoDavanjuuZakup
                    select e).ToList();
        }

        public OdlukaoDavanjuuZakup GetOdlukaById(Guid OdlukaoDavanjuuZakupId)
        {
            return odlukeoDavanjuuZakup.FirstOrDefault(e => e.OdlukaoDavanjuuZakupID == OdlukaoDavanjuuZakupId);
        }

        public OdlukaoDavanjuuZakupConfirmation CreateOdluka(OdlukaoDavanjuuZakup OdlukaoDavanjuuZakup)
        { 
            OdlukaoDavanjuuZakup.OdlukaoDavanjuuZakupID = Guid.NewGuid();
            odlukeoDavanjuuZakup.Add(OdlukaoDavanjuuZakup);
            OdlukaoDavanjuuZakup odluka = GetOdlukaById(OdlukaoDavanjuuZakup.OdlukaoDavanjuuZakupID);

            return new OdlukaoDavanjuuZakupConfirmation
            {
                OdlukaoDavanjuuZakupID = odluka.OdlukaoDavanjuuZakupID,
                validnost = odluka.validnost
            };
        }

        public OdlukaoDavanjuuZakupConfirmation UpdateOdluka(OdlukaoDavanjuuZakup OdlukaoDavanjuuZakup)
        {
            OdlukaoDavanjuuZakup odluka = GetOdlukaById(OdlukaoDavanjuuZakup.OdlukaoDavanjuuZakupID);

            odluka.OdlukaoDavanjuuZakupID = OdlukaoDavanjuuZakup.OdlukaoDavanjuuZakupID;
            odluka.datum_donosenja_odluke = OdlukaoDavanjuuZakup.datum_donosenja_odluke;
            odluka.validnost = OdlukaoDavanjuuZakup.validnost;
            return new OdlukaoDavanjuuZakupConfirmation
            {
                OdlukaoDavanjuuZakupID = odluka.OdlukaoDavanjuuZakupID,
                validnost = odluka.validnost
            };
        }

        public void DeleteOdluka(Guid OdlukaoDavanjuuZakupId)
        {
            odlukeoDavanjuuZakup.Remove(odlukeoDavanjuuZakup.FirstOrDefault(e => e.OdlukaoDavanjuuZakupID == OdlukaoDavanjuuZakupId));
        }

       
    }
}
