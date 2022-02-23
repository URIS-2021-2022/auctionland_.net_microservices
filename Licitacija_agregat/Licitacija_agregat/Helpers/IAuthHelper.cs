using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Helpers
{
    public interface IAuthHelper
    {
        bool AuthenticatePrincipal(Principal principal);
        string GenerateJwt(Principal principal);
    }
}
