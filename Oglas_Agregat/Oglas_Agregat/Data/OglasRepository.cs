using AutoMapper;
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

        private readonly OglasContext context;
        private readonly IMapper mapper;


        public OglasRepository(OglasContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public OglasConfirmation CreateOglas(Oglas oglasModel)
        {
            var createdEntity = context.Add(oglasModel);
            return mapper.Map<OglasConfirmation>(createdEntity.Entity);

        }

        public void DeleteOglas(Guid OglasId)
        {
            context.Remove(GetOglasById(OglasId));
        }

        public Oglas GetOglasById(Guid OglasId)
        {
            return context.Oglasi.FirstOrDefault(e => e.OglasId == OglasId);

        }

        public List<Oglas> GetOglasi(DateTime DatumObjave = default)
        {
            return context.Oglasi.Where(e => (DatumObjave == default || e.DatumObjave.Equals(DatumObjave))).ToList();
        }

        public void UpdateOglas(Oglas oglasModel)
        {
/*            var oglas = GetOglasById(oglasModel.OglasId);

            oglas.OglasId = oglasModel.OglasId;
            oglas.DatumObjave = oglasModel.DatumObjave;
            oglas.RokZaZalbu = oglasModel.RokZaZalbu;
            oglas.OpisOglasa = oglasModel.OpisOglasa;

            return new OglasConfirmation()
            {
                OglasId = oglas.OglasId,
                OpisOglasa = oglas.OpisOglasa
            };*/
        }
    }
}
