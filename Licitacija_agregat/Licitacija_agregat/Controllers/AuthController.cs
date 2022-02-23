using Licitacija_agregat.Data;
using Licitacija_agregat.Helpers;
using Licitacija_agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHelper authHelper;
        private readonly ILoggerService loggerService;

        public AuthController(IAuthHelper authnHelper, ILoggerService loggerService)
        {
            this.authHelper = authnHelper;
            this.loggerService = loggerService;
        }


        [HttpPost("login")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] Principal principal)
        {

            if (authHelper.AuthenticatePrincipal(principal))
            {
                var tokenString = authHelper.GenerateJwt(principal);

                loggerService.Log(LogLevel.Information, "PostStatus", "Token je uspešno vraćen!");

                return Ok(new { token = tokenString });
            }

            loggerService.Log(LogLevel.Warning, "GetAllStatus", "Token nije vraćen, korisnik nije autorizovan!.");

            return Unauthorized();
        }
    }
}
