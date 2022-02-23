using Parcela.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.ServiceCalls
{
    public interface IFizickoLiceRepository
    {
        Task<FizickoLiceDto> GetFizickoLiceByIdAsync(Guid FizickoLiceId, HttpRequest httpRequest);

    }
}