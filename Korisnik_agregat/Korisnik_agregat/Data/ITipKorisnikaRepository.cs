using Korisnik_agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Data
{
    public interface ITipKorisnikaRepository
    {
        List<TipKorisnika> GetTipKorisnikaList();
        TipKorisnika GetTipKorisnikaById(Guid tipKorisnikaId);
        bool SaveChanges();

    }
}
