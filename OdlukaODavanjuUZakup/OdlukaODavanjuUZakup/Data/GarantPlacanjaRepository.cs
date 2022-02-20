using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public class GarantPlacanjaRepository : IGarantPlacanjaRepository
    {
        public static List<GarantPlacanja> garantiPlacanja { get; set; } = new List<GarantPlacanja>();

        public GarantPlacanjaRepository()
        {
            FillData();
        }
        private void FillData()
        {

        }

        public GarantPlacanjaConfirmation CreateGarantPlacanja(GarantPlacanja garantPlacanja)
        {
            garantPlacanja.GarantPlacanjaID = Guid.NewGuid();
            garantiPlacanja.Add(garantPlacanja);
            GarantPlacanja garant = GetGarantPlacanjaById(garantPlacanja.GarantPlacanjaID);

            return new GarantPlacanjaConfirmation
            {
                GarantPlacanjaID = garant.GarantPlacanjaID,
                Opis_garanta1 = garant.Opis_garanta1

            };
        }

        public void DeleteGarantPlacanja(Guid GarantPlacanjaId)
        {
            garantiPlacanja.Remove(garantiPlacanja.FirstOrDefault(e => e.GarantPlacanjaID == GarantPlacanjaId));
        }

        public List<GarantPlacanja> GetGarantiPlacanja()
        {
            return (from e in garantiPlacanja
                    select e).ToList();
        }

        public GarantPlacanja GetGarantPlacanjaById(Guid GarantPlacanjaId)
        {
            return garantiPlacanja.FirstOrDefault(e => e.GarantPlacanjaID == GarantPlacanjaId);
        }

        public GarantPlacanjaConfirmation UpdateGarantPlacanja(GarantPlacanja garantPlacanja)
        {
            GarantPlacanja garant = GetGarantPlacanjaById(garantPlacanja.GarantPlacanjaID);

            garant.GarantPlacanjaID = garantPlacanja.GarantPlacanjaID;
            garant.Opis_garanta1 = garantPlacanja.Opis_garanta1;
            garant.Opis_garanta2 = garantPlacanja.Opis_garanta2;

            return new GarantPlacanjaConfirmation
            {
                GarantPlacanjaID = garant.GarantPlacanjaID,
                Opis_garanta1 = garant.Opis_garanta1

            };
        }
    }
}
