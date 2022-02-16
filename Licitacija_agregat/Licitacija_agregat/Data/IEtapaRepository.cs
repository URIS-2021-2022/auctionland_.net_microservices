using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public interface IEtapaRepository
    {
        List<EtapaModel> GetEtapas(DateTime dan = default);
        EtapaModel GetEtapaById(Guid EtapaId);
        EtapaConfirmation CreateEtapa(EtapaModel etapaModel);
        EtapaConfirmation UpdateEtapa(EtapaModel etapaModel);
        void DeleteEtapa(Guid EtapaId);
    }
}
