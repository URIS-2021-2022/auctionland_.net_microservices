using System;
using Komisija_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Entities;

namespace Komisija_Agregat.Data
{
    public interface IClanKomisijeRepository
    {
        List<ClanKomisije> GetClanovi(string ImeClana = null, string PrezimeClana = null, string EmailClana = null);

        ClanKomisije GetClanKomisijeById(Guid ClanId);

        ClanKomisijeConfirmation CreateClanKomisije(ClanKomisije clanKomisije);

        ClanKomisijeConfirmation UpdateClanKomisije(ClanKomisije clanKomisije);

        void DeleteClanKomisije(Guid ClanId);
    }
}
