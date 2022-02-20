using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public interface IUgovoroZakupuRepository
    {
        List<UgovoroZakupuModel> GetUgovoriOZakupu(string zavodni_broj = null);

        UgovoroZakupuModel GetUgovoriOZakupuById(Guid UgovoroZakupuId);

        UgovoroZakupuConfirmation CreateUgovorOZakupu(UgovoroZakupuModel ugovoroZakupu);

        UgovoroZakupuConfirmation UpdateUgovorOZakupu(UgovoroZakupuModel ugovoroZakupu);

        void DeleteUgovorOZakupu(Guid UgovoroZakupuId);
    }
}
