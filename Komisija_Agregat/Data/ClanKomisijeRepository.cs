using Komisija_Agregat.Models;
using System;

namespace Komisija_Agregat.Data
{

    public class ClanKomisijeRepository : IClanKomisijeRepository
    {
        public static List<ClanKomisijeModel> ClanoviKomisije { get; set; } = new List<ClanKomisijeModel>();

        public ClanKomisijeRepository()
        {
            FillData();
        }

        private void FillData()
        {
            ClanoviKomisije.AddRange(new List<ClanKomisijeModel>
            {
                new ClanKomisijeModel
                {
                   ClanId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                   ImeClana = "Aleksa",
                   PrezimeClana = "Pivnički",
                   EmailClana = "pivna@mail.com"
                },
                new ClanKomisijeModel
                {
                   ClanId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   ImeClana = "Konstantin",
                   PrezimeClana = "Jekić",
                   EmailClana = "jekavac@mail.com"
                }
            });
        }

        public List<ClanKomisijeModel> GetClanovi(string ImeClana = null, string PrezimeClana = null, string EmailClana = null)
        {

            return (from e in ClanoviKomisije
                    where string.IsNullOrEmpty(ImeClana) || e.ImeClana == ImeClana &&
                          string.IsNullOrEmpty(PrezimeClana) || e.PrezimeClana == PrezimeClana &&
                          string.IsNullOrEmpty(EmailClana) || e.EmailClana == EmailClana
                    select e).ToList();
        }

        public ClanKomisijeModel GetClanKomisijeById(Guid ClanId)
        {

            return ClanoviKomisije.FirstOrDefault(e => e.ClanId == ClanId);

        }

        public ClanKomisijeConfirmationDto CreateClanKomisije(ClanKomisijeModel clanKomisije)
        {
            clanKomisije.ClanId = Guid.NewGuid();
            ClanoviKomisije.Add(clanKomisije);
            ClanKomisijeModel clan = GetClanKomisijeById(clanKomisije.ClanId);

            return new ClanKomisijeConfirmationDto
            {
                ClanId = clan.ClanId,

                ImeClana = clan.ImeClana,

                PrezimeClana = clan.PrezimeClana,

                EmailClana = clan.EmailClana

            };
        }

        public ClanKomisijeConfirmationDto UpdateClanKomisije(ClanKomisijeModel clanKomisije)
        {
            ClanKomisijeModel clan = GetClanKomisijeById(clanKomisije.ClanId);

            clan.ClanId = clanKomisije.ClanId;
            clan.ImeClana = clanKomisije.ImeClana;
            clan.PrezimeClana = clanKomisije.PrezimeClana;
            clan.EmailClana = clanKomisije.EmailClana;

            return new ClanKomisijeConfirmationDto
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
