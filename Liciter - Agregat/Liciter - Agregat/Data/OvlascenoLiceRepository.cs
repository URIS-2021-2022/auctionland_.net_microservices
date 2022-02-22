using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class OvlascenoLiceRepository : IOvlascenoLiceRepository
    {
        public List<OvlascenoLiceModel> ovlascenaLica { get; set; } = new List<OvlascenoLiceModel>();

        public OvlascenoLiceRepository()
        {
            FillData();
        }

        private void FillData()
        {

        }
        public OvlascenoLiceConfirmation CreateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
            ovlascenoLice.OvlascenoLiceId = Guid.NewGuid();
            ovlascenaLica.Add(ovlascenoLice);
            OvlascenoLiceModel lice = GetOvlascenoLiceById(ovlascenoLice.OvlascenoLiceId);

            return new OvlascenoLiceConfirmation
            {
               OvlascenoLiceId = lice.OvlascenoLiceId,
               Ime = lice.Ime,
               Prezime = lice.Prezime

            };
        }

        public void DeleteOvlascenoLice(Guid OvlascenoLiceId)
        {
            ovlascenaLica.Remove(ovlascenaLica.FirstOrDefault(e => e.OvlascenoLiceId == OvlascenoLiceId));
        }

        public List<OvlascenoLiceModel> GetOvlascenaLicas(string JMBG_BrPasosa = null)
        {
            return (from e in ovlascenaLica
                    where string.IsNullOrEmpty(JMBG_BrPasosa) || e.JMBG_Br_Pasosa == JMBG_BrPasosa
                    select e).ToList();
        }

        public OvlascenoLiceModel GetOvlascenoLiceById(Guid OvlascenoLiceId)
        {
            return ovlascenaLica.FirstOrDefault(e => e.OvlascenoLiceId == OvlascenoLiceId);
        }

        public OvlascenoLiceConfirmation UpdateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
            OvlascenoLiceModel lice = GetOvlascenoLiceById(ovlascenoLice.OvlascenoLiceId);

            lice.Adresa = ovlascenoLice.Adresa;
            lice.BrojTable = ovlascenoLice.BrojTable;
            lice.Drzava = ovlascenoLice.Drzava;
            lice.Ime = ovlascenoLice.Ime;
            lice.JMBG_Br_Pasosa = ovlascenoLice.JMBG_Br_Pasosa;
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
