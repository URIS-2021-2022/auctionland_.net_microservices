using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public interface ILicitacijaRepository
    {
        List<LicitacijaModel> GetLicitacijas(DateTime datum = default);
        LicitacijaModel GetLicitacijaById(Guid LicitacijaId);
        LicitacijaConfirmation CreateLicitacija(LicitacijaModel licitacijaModel);
        LicitacijaConfirmation UpdateLicitacija(LicitacijaModel licitacijaModel);
        void DeleteLicitacija(Guid LicitacijaId);

    }
}
