using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    interface IGarantPlacanjaRepository
    {
        List<GarantPlacanjaModel> GetGarantiPlacanja();

        GarantPlacanjaModel GetGarantPlacanjaById(Guid GarantPlacanjaId);

        GarantPlacanjaConfirmation CreateGarantPlacanja(GarantPlacanjaModel garantPlacanja);

        GarantPlacanjaConfirmation UpdateGarantPlacanja(GarantPlacanjaModel garantPlacanja);

        void DeleteGarantPlacanja(Guid GarantPlacanjaId);
    }
}
