using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public interface IOglasRepository
    {
        List<OglasModel> GetOglasi(DateTime DatumObjave = default);
        OglasModel GetOglasById(Guid OglasId);
        OglasConfirmation CreateOglas(OglasModel oglasModel);
        OglasConfirmation UpdateOglas(OglasModel oglasModel);
        void DeleteOglas(Guid OglasId);


    }
}
