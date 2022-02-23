using Zalba.Data;
using Zalba.Helpers;
using Zalba.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHelper authHelper;
        private readonly ILoggerService loggerService;

        public AuthController(IAuthHelper authHelper, ILoggerService loggerService)
        {
            this.authHelper = authHelper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Služi za autentifikaciju korisnika
        /// </summary>
        /// <param name="principal">Model sa podacima na osnovu kojih se vrši autentifikacija</param>
        /// <returns></returns>
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