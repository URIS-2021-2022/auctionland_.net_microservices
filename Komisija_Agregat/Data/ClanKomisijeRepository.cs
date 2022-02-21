using Komisija_Agregat.Models;
using Komisija_Agregat.Entities;
using Komisija_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Komisija_Agregat.Data
{

    public class ClanKomisijeRepository : IClanKomisijeRepository
    {
        public static List<ClanKomisije> ClanoviKomisije { get; set; } = new List<ClanKomisije>();

        public ClanKomisijeRepository()
        {
            FillData();
        }

        private void FillData()
        {
          
        }

        public List<ClanKomisije> GetClanovi(string ImeClana = null, string PrezimeClana = null, string EmailClana = null)
        {

            return (from e in ClanoviKomisije
                    where string.IsNullOrEmpty(ImeClana) || e.ImeClana == ImeClana &&
                          string.IsNullOrEmpty(PrezimeClana) || e.PrezimeClana == PrezimeClana &&
                          string.IsNullOrEmpty(EmailClana) || e.EmailClana == EmailClana
                    select e).ToList();
        }

        public ClanKomisije GetClanKomisijeById(Guid ClanId)
        {

            return ClanoviKomisije.FirstOrDefault(e => e.ClanId == ClanId);

        }

        public ClanKomisijeConfirmation CreateClanKomisije(ClanKomisije clanKomisije)
        {
            clanKomisije.ClanId = Guid.NewGuid();
            ClanoviKomisije.Add(clanKomisije);
            ClanKomisije clan = GetClanKomisijeById(clanKomisije.ClanId);

            return new ClanKomisijeConfirmation
            {
                ClanId = clan.ClanId,

                ImeClana = clan.ImeClana,

                PrezimeClana = clan.PrezimeClana,

                EmailClana = clan.EmailClana

            };
        }

        public ClanKomisijeConfirmation UpdateClanKomisije(ClanKomisije clanKomisije)
        {
            ClanKomisije clan = GetClanKomisijeById(clanKomisije.ClanId);

            clan.ClanId = clanKomisije.ClanId;
            clan.ImeClana = clanKomisije.ImeClana;
            clan.PrezimeClana = clanKomisije.PrezimeClana;
            clan.EmailClana = clanKomisije.EmailClana;

            return new ClanKomisijeConfirmation
            {
                ClanId = clan.ClanId,
                ImeClana = clan.ImeClana,
                PrezimeClana = clan.PrezimeClana,
                EmailClana = clan.EmailClana
            };
        }

        public void DeleteClanKomisije(Guid ClanId)
        {
            ClanoviKomisije.Remove(ClanoviKomisije.FirstOrDefault(e => e.ClanId == ClanId));
        }

    }
}
