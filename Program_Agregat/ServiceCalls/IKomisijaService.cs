using Microsoft.AspNetCore.Http;
using Program_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.ServiceCalls
{
    public interface IKomisijaService
    {
        Task<KomisijaDto> GetKomisijaByIdAsync(Guid komisijaId, HttpRequest httpRequest);
    }
}