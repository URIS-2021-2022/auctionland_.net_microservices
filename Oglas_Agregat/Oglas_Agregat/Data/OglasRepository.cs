using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public class OglasRepository : IOglasRepository
    {
        public static List<OglasModel> Oglasi { get; set; } = new List<OglasModel>();

        public OglasRepository()
        {
            FillData();
        }

        private void FillData()
        {
            
        }

        public OglasConfirmation CreateOglas(OglasModel oglasModel)
        {
            oglasModel.OglasId = Guid.NewGuid();
            Oglasi.Add(oglasModel);
            var oglas = GetOglasById(oglasModel.OglasId);

            return new OglasConfirmation()
            {
                OglasId = oglas.OglasId,
                OpisOglasa = oglas.OpisOglasa
            };

        }

        public void DeleteOglas(Guid OglasId)
        {
            Oglasi.Remove(Oglasi.FirstOrDefault(e => e.OglasId == OglasId));
        }

        public OglasModel GetOglasById(Guid OglasId)
        {
            return Oglasi.FirstOrDefault(e => e.OglasId == OglasId);
    }

        public List<OglasModel> GetOglasi(DateTime DatumObjave = default)
        {
            return (from e in Oglasi
                    where DatumObjave == default || e.DatumObjave.Equals(DatumObjave)
                    select e).ToList();
        }

        public OglasConfirmation UpdateOglas(OglasModel oglasModel)
        {
            var oglas = GetOglasById(oglasModel.OglasId);

            oglas.OglasId = oglasModel.OglasId;
            oglas.DatumObjave = oglasModel.DatumObjave;
            oglas.OpisOglasa = oglasModel.OpisOglasa;

            return new OglasConfirmation()
            {
                OglasId = oglas.OglasId,
                OpisOglasa = oglas.OpisOglasa
            };
        }
    }
}
