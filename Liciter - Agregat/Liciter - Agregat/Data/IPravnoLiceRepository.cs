using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
   public interface IPravnoLiceRepository
    {
        List<PravnoLiceModel> GetPravnaLicas(string MaticniBroj = null);

        PravnoLiceModel GetPravnoLiceById(Guid PravnoLiceId);

        PravnoLiceConfirmation CreatePravnoLice(PravnoLiceModel pravnoLice);

        PravnoLiceConfirmation UpdatePravnoLice(PravnoLiceModel pravnoLice);

        void DeletePravnoLice(Guid PravnoLiceId);

        bool SaveChanges();
    }
}
