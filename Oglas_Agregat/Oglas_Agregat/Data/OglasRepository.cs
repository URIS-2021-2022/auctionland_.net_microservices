using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public class OglasRepository : IOglasRepository
    {
        public static List<Oglas> Oglasi { get; set; } = new List<Oglas>();
        

        public OglasRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Oglasi.AddRange(new List<Oglas>
            {
                new Oglas
                {
                    OglasId = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    OpisOglasa = "fdafdafa",
                    RokZaZalbu = DateTime.Parse("02-02-2000"),
                    DatumObjave = DateTime.Parse("01-01-2000")
                }
            });
        }

        public OglasConfirmation CreateOglas(Oglas oglasModel)
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

        public Oglas GetOglasById(Guid OglasId)
        {
            return Oglasi.FirstOrDefault(e => e.OglasId == OglasId);
    }

        public List<Oglas> GetOglasi(DateTime DatumObjave = default)
        {
            return (from e in Oglasi
                    where DatumObjave == default || e.DatumObjave.Equals(DatumObjave)
                    select e).ToList();
        }

        public OglasConfirmation UpdateOglas(Oglas oglasModel)
        {
            var oglas = GetOglasById(oglasModel.OglasId);

            oglas.OglasId = oglasModel.OglasId;
            oglas.DatumObjave = oglasModel.DatumObjave;
            oglas.RokZaZalbu = oglasModel.RokZaZalbu;
            oglas.OpisOglasa = oglasModel.OpisOglasa;

            return new OglasConfirmation()
            {
                OglasId = oglas.OglasId,
                OpisOglasa = oglas.OpisOglasa
            };
        }
    }
}
