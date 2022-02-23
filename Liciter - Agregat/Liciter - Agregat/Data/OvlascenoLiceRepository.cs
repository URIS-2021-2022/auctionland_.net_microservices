using AutoMapper;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class OvlascenoLiceRepository : IOvlascenoLiceRepository
    {
        private readonly DataBaseContext context;
        private readonly IMapper mapper;

        public OvlascenoLiceRepository(DataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public OvlascenoLiceConfirmation CreateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
            var createdEntity = context.Add(ovlascenoLice);
            return mapper.Map<OvlascenoLiceConfirmation>(createdEntity.Entity);
        }

        public void DeleteOvlascenoLice(Guid OvlascenoLiceId)
        {
            var ovlascenoLice = GetOvlascenoLiceById(OvlascenoLiceId);
            context.Remove(ovlascenoLice);
        }

        public List<OvlascenoLiceModel> GetOvlascenaLicas(string JMBG_BrPasosa = null)
        {
            return context.OvlascenaLica.Where(e => (JMBG_BrPasosa == null || e.JMBG_Br_Pasosa == JMBG_BrPasosa)).ToList();
        }

        public OvlascenoLiceModel GetOvlascenoLiceById(Guid OvlascenoLiceId)
        {
            return context.OvlascenaLica.FirstOrDefault(e => e.OvlascenoLiceId == OvlascenoLiceId);
        }

        public OvlascenoLiceConfirmation UpdateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
            OvlascenoLiceModel lice = GetOvlascenoLiceById(ovlascenoLice.OvlascenoLiceId);

            lice.Adresa = ovlascenoLice.Adresa;
            lice.BrojTable = ovlascenoLice.BrojTable;
            lice.Drzava = ovlascenoLice.Drzava;
            lice.Ime = ovlascenoLice.Ime;
            lice.JMBG_Br_Pasosa = ovlascenoLice.JMBG_Br_Pasosa;
            lice.Kupac = ovlascenoLice.Kupac;
            lice.KupacId = ovlascenoLice.KupacId;
            lice.OvlascenoLiceId = ovlascenoLice.OvlascenoLiceId;
            lice.Prezime = ovlascenoLice.Prezime;

            return new OvlascenoLiceConfirmation
            {
                OvlascenoLiceId = lice.OvlascenoLiceId,
                Ime = lice.Ime,
                Prezime = lice.Prezime
            };
        }
    }
}
