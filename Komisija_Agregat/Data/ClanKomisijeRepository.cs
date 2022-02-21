using Komisija_Agregat.Models;
using Komisija_Agregat.Entities;
using Komisija_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using AutoMapper;

namespace Komisija_Agregat.Data
{

    public class ClanKomisijeRepository : IClanKomisijeRepository
    {

        private readonly KomisijaContext context;
        private readonly IMapper mapper;

        public ClanKomisijeRepository(KomisijaContext context, IMapper mapper)
        {
            
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<ClanKomisije> GetClanovi(string ImeClana = null, string PrezimeClana = null, string EmailClana = null)
        {

            return context.ClanoviKomisije.Where( e=> string.IsNullOrEmpty(ImeClana) || e.ImeClana == ImeClana &&
                          string.IsNullOrEmpty(PrezimeClana) || e.PrezimeClana == PrezimeClana &&
                          string.IsNullOrEmpty(EmailClana) || e.EmailClana == EmailClana).ToList();
        }

        public ClanKomisije GetClanKomisijeById(Guid ClanId)
        {

            return context.ClanoviKomisije.FirstOrDefault(e => e.ClanId == ClanId);

        }

        public ClanKomisijeConfirmation CreateClanKomisije(ClanKomisije clanKomisije)
        {
            var createdEntity = context.Add(clanKomisije);
            return mapper.Map<ClanKomisijeConfirmation>(createdEntity.Entity);
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
            context.Remove(GetClanKomisijeById(ClanId));
        }

    }
}
