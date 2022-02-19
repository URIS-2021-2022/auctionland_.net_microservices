using System;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Data
{
    public interface IClanKomisijeRepository
    {
        List<ClanKomisijeModel> GetClanovi(string ImeClana = null, string PrezimeClana = null, string EmailClana = null);

        ClanKomisijeModel GetClanKomisijeById(Guid ClanId);

        ClanKomisijeConfirmation CreateClanKomisije(ClanKomisijeModel clanKomisije);

        ClanKomisijeConfirmation UpdateClanKomisije(ClanKomisijeModel clanKomisije);

        void DeleteClanKomisije(Guid ClanId);
    }
}
