using Korisnik_agregat.Helpers;
using Korisnik_agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IAuthHelper authHelper)
        {
            this.authHelper = authHelper;
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


                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
    }
}
