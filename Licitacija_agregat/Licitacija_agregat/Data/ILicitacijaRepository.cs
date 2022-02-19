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
        void UpdateLicitacija(Licitacija licitacijaModel);
        void DeleteLicitacija(Guid LicitacijaId);

        bool SaveChanges();
    }
}
