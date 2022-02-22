using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
   public interface IKupacRepository
    {
        List<KupacModel> GetKupci(string JMBG_MaticniBroj = null);

        KupacModel GetKupacById(Guid KupacId);

        KupacConfirmation CreateKupac(KupacModel kupac);

        KupacConfirmation UpdateKupac(KupacModel kupac);

        void DeleteKupac(Guid KupacId);
    }
}
