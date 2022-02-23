using Zalba.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.ServiceCalls
{
    public interface IFizickoLiceRepository
    {
        Task<FizickoLiceDto> GetFizickoLiceByIdAsync(Guid FizickoLiceId, HttpRequest httpRequest);

    }
}