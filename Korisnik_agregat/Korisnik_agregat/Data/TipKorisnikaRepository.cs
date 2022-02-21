using Korisnik_agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Data
{
    class TipKorisnikaRepository : ITipKorisnikaRepository
    {
        private readonly KorisnikContext context;

        public TipKorisnikaRepository(KorisnikContext context)
        {
            this.context = context;
        }

        public List<TipKorisnika> GetTipKorisnikaList()
        {
            return context.TipoviKorisnika.ToList();
        }

        public TipKorisnika GetTipKorisnikaById(Guid tipKorisnikaId)
        {
            return context.TipoviKorisnika.FirstOrDefault(e => e.TipKorisnikaId == tipKorisnikaId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
