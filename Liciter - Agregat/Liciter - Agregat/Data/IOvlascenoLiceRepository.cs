using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    interface IOvlascenoLiceRepository
    {
        List<OvlascenoLiceModel> GetOvlascenaLicas(string JMBG_BrPasosa = null);

        OvlascenoLiceModel GetOvlascenoLiceById(Guid OvlascenoLiceId);

        OvlascenoLiceConfirmation CreateOvlascenoLice(OvlascenoLiceModel ovlascenoLice);

        OvlascenoLiceConfirmation UpdateOvlascenoLice(OvlascenoLiceModel ovlascenoLice);

        void DeleteOvlascenoLice(Guid OvlascenoLiceId);
    }
}
