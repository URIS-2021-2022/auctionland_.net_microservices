using Microsoft.AspNetCore.Http;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.ServiceCalls
{
    public interface IJavnoNadmetanjeService
    {
        Task<JavnoNadmetanjeDto> GetJavnoNadmetanjeByIdAsync(Guid javnoNadmetanjeId, HttpRequest httpRequest);
    }
}