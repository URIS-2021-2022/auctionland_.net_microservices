using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public interface IGarantPlacanjaRepository
    {
        public bool SaveChanges();
        List<GarantPlacanja> GetGarantiPlacanja();

        GarantPlacanja GetGarantPlacanjaById(Guid GarantPlacanjaId);

        GarantPlacanjaConfirmation CreateGarantPlacanja(GarantPlacanja garantPlacanja);

        GarantPlacanjaConfirmation UpdateGarantPlacanja(GarantPlacanja garantPlacanja);

        void DeleteGarantPlacanja(Guid GarantPlacanjaId);
    }
}
