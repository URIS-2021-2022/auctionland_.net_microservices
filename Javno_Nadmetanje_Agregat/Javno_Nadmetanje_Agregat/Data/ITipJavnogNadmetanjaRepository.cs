using Javno_Nadmetanje_Agregat.Entities;
using Javno_Nadmetanje_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Data
{
    public interface ITipJavnogNadmetanjaRepository
    {
        List<TipJavnogNadmetanja> GetTipJavnogNadmetanjaList();
        TipJavnogNadmetanja GetTipJavnogNadmetanjaById(Guid tipJavnogNadmetanjaId);
        TipJavnogNadmetanjaConfirmationDto CreateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja);
        TipJavnogNadmetanjaConfirmationDto UpdateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja);
        TipJavnogNadmetanjaConfirmationDto DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaId);
    }
}
