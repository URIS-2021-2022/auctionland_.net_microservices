using Licitacija_agregat.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.ServiceCalls
{
    public interface IProgramRepository
    {
       Task<ProgramDto> GetProgramByIdAsync(Guid ProgramId, HttpRequest httpRequest);

    }
}
