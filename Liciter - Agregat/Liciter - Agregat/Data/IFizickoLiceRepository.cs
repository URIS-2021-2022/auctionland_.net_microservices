using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    interface IFizickoLiceRepository
    {
        List<FizickoLiceModel> GetFizickaLicas(string JMBG = null);

        FizickoLiceModel GetFizickoLiceById(Guid FizickoLiceId);

        FizickoLiceConfirmation CreateFizickoLice(FizickoLiceModel fizickoLice);

        FizickoLiceConfirmation UpdateFizickoLice(FizickoLiceModel fizickoLice);

        void DeleteFizickoLice(Guid FizickoLiceId);
    }
}
