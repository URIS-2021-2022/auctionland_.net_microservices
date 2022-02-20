using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    /// <summary>
    /// Interfejs za SluzbeniListRepository
    /// </summary>
    public interface ISluzbeniListRepository
    {
        List<SluzbeniList> GetSluzbeniListovi(int BrojLista = default);
        SluzbeniList GetSluzbeniListById(Guid SluzbeniListId);
        SluzbeniListConfirmation CreateSluzbeniList(SluzbeniList sluzbeniList);
        void UpdateSluzbeniList(SluzbeniList sluzbeniList);
        void DeleteSluzbeniList(Guid SluzbeniListId);

        bool SaveChanges();
    }
}
