using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    interface IOdlukaoDavanjuuZakupRepository
    {
        List<OdlukaoDavanjuuZakupModel> GetOdluke();

        OdlukaoDavanjuuZakupModel GetOdlukaById(Guid OdlukaoDavanjuuZakupId);

        OdlukaoDavanjuuZakupConfirmation CreateOdluka(OdlukaoDavanjuuZakupModel OdlukaoDavanjuuZakup);

        OdlukaoDavanjuuZakupConfirmation UpdateOdluka(OdlukaoDavanjuuZakupModel OdlukaoDavanjuuZakup);

        void DeleteOdluka(Guid OdlukaoDavanjuuZakupId);
    }
}
