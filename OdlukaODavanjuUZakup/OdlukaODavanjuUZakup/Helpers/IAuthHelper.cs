using Microsoft.AspNetCore.Http;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Helpers
{
    public interface IAuthHelper
    {
        bool AuthenticatePrincipal(Principal principal);
        string GenerateJwt(Principal principal);
        string GetToken(HttpRequest request);
    }
}
