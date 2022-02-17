using Licitacija_agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public interface ILicitacijaRepository
    {
        List<Licitacija> GetLicitacijas(DateTime datum = default);
        Licitacija GetLicitacijaById(Guid LicitacijaId);
        LicitacijaConfirmation CreateLicitacija(Licitacija licitacijaModel);
        LicitacijaConfirmation UpdateLicitacija(Licitacija licitacijaModel);
        void DeleteLicitacija(Guid LicitacijaId);

    }
}
