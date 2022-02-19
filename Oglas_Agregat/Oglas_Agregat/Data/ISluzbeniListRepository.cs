using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public interface ISluzbeniListRepository
    {
        List<SluzbeniList> GetSluzbeniListovi(int BrojLista = default);
        SluzbeniList GetSluzbeniListById(Guid SluzbeniListId);
        SluzbeniListConfirmation CreateSluzbeniList(SluzbeniList sluzbeniList);
        SluzbeniListConfirmation UpdateSluzbeniList(SluzbeniList sluzbeniList);
        void DeleteSluzbeniList(Guid SluzbeniListId);
    }
}
