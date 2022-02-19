using System;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Data
{
    public interface IKomisijaRepository
    {
        List<KomisijaModel> GetKomisije();

        KomisijaModel GetKomisijaById(Guid KomisijaId);

        KomisijaConfirmation CreateKomisija(KomisijaModel komisija);

        KomisijaConfirmation UpdateKomisija(KomisijaModel komisija);

        void DeleteKomisija(Guid KomisijaId);
    }
}
