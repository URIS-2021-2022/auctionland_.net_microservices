using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public interface IUgovoroZakupuRepository
    {
        List<UgovoroZakupu> GetUgovoriOZakupu(string zavodni_broj = null);

        UgovoroZakupu GetUgovoriOZakupuById(Guid UgovoroZakupuId);

        UgovoroZakupuConfirmation CreateUgovorOZakupu(UgovoroZakupu ugovoroZakupu);

        UgovoroZakupuConfirmation UpdateUgovorOZakupu(UgovoroZakupu ugovoroZakupu);

        void DeleteUgovorOZakupu(Guid UgovoroZakupuId);
    }
}
