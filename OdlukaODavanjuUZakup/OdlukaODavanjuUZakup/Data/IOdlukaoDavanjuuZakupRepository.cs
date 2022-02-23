using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public interface IOdlukaoDavanjuuZakupRepository
    {
        public bool SaveChanges();
        List<OdlukaoDavanjuuZakup> GetOdluke();

        OdlukaoDavanjuuZakup GetOdlukaById(Guid OdlukaoDavanjuuZakupId);

        OdlukaoDavanjuuZakupConfirmation CreateOdluka(OdlukaoDavanjuuZakup OdlukaoDavanjuuZakup);

        OdlukaoDavanjuuZakupConfirmation UpdateOdluka(OdlukaoDavanjuuZakup OdlukaoDavanjuuZakup);

        void DeleteOdluka(Guid OdlukaoDavanjuuZakupId);
    }
}
