using System;
using Komisija_Agregat.Models;
using Komisija_Agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Data
{
    public interface IKomisijaRepository
    {
        List<Komisija> GetKomisije();

        Komisija GetKomisijaById(Guid KomisijaId);

        KomisijaConfirmation CreateKomisija(Komisija komisija);

        KomisijaConfirmation UpdateKomisija(Komisija komisija);

        void DeleteKomisija(Guid KomisijaId);

        bool SaveChanges();
    }
}