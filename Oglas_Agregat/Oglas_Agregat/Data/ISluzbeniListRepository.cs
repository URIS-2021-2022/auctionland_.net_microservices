using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public interface ISluzbeniListRepository
    {
        List<SluzbeniListModel> GetSluzbeniListovi(int BrojLista = default);
        SluzbeniListModel GetSluzbeniListById(Guid SluzbeniListId);
        SluzbeniListConfirmation CreateSluzbeniList(SluzbeniListModel sluzbeniListModel);
        SluzbeniListConfirmation UpdateSluzbeniList(SluzbeniListModel sluzbeniListModel);
        void DeleteSluzbeniList(Guid SluzbeniListId);
    }
}
