using Parcela.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.ServiceCalls
{
    public interface IPravnoLiceRepository
    {
        Task<PravnoLiceDto> GetPravnoLiceByIdAsync(Guid PravnoLiceId, HttpRequest httpRequest);

    }
}