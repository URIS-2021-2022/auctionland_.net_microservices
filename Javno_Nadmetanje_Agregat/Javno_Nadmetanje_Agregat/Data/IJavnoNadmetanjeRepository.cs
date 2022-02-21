using Javno_Nadmetanje_Agregat.Entities;
using Javno_Nadmetanje_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Data
{
    public interface IJavnoNadmetanjeRepository
    {
        List<JavnoNadmetanje> GetJavnoNadmetanjeList();
        JavnoNadmetanje GetJavnoNadmetanjeById(Guid javnoNadmetanjeId);
        JavnoNadmetanjeConfirmationDto CreateJavnoNadmetanje(JavnoNadmetanje javnoNadmetanje);
        JavnoNadmetanjeConfirmationDto UpdateJavnoNadmetanje(JavnoNadmetanje javnoNadmetanje);
        JavnoNadmetanjeConfirmationDto DeleteJavnoNadmetanje(Guid javnoNadmetanjeId);
    }
}
