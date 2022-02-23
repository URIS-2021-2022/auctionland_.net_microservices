using Liciter___Agregat.DTOs.Kupac;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public interface IJavnaNadmetanjaService
    {
        JavnoNadmetanjeDto GetJavnoNadmetanjeById(Guid javnoNadmetanjeId, HttpRequest httpRequest);
    }
}
