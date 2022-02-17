using Licitacija_agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public interface IEtapaRepository
    {
        List<Etapa> GetEtapas(DateTime dan = default);
        Etapa GetEtapaById(Guid EtapaId);
        EtapaConfirmation CreateEtapa(Etapa etapaModel);
        EtapaConfirmation UpdateEtapa(Etapa etapaModel);
        void DeleteEtapa(Guid EtapaId);
    }
}
