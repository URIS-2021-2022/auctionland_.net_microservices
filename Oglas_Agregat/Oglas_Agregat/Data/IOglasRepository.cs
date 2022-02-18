using Oglas_Agregat.Entities; //ne koristim vise models nego entities
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public interface IOglasRepository
    {
        List<Oglas> GetOglasi(DateTime DatumObjave = default);
        Oglas GetOglasById(Guid OglasId);
        OglasConfirmation CreateOglas(Oglas oglas);
        OglasConfirmation UpdateOglas(Oglas oglas);
        void DeleteOglas(Guid OglasId);


    }
}
