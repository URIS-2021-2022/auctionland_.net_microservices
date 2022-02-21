using Javno_Nadmetanje_Agregat.Entities;
using Javno_Nadmetanje_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Data
{
    public interface IStatusJavnogNadmetanjaRepository
    {
        List<StatusJavnogNadmetanja> GetStatusJavnogNadmetanjaList();
        StatusJavnogNadmetanja GetStatusJavnogNadmetanjaById(Guid statusJavnogNadmetanjaId);
        StatusJavnogNadmetanjaConfirmationDto CreateStatusJavnogNadmetanja(StatusJavnogNadmetanja statusJavnogNadmetanja);
        StatusJavnogNadmetanjaConfirmationDto UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanja statusJavnogNadmetanja);
        StatusJavnogNadmetanjaConfirmationDto DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaId);
    }
}
