using AutoMapper;
using Korisnik_agregat.Data;
using Korisnik_agregat.Entities;
using Korisnik_agregat.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TipKorisnikaController : ControllerBase
    {
        private readonly ITipKorisnikaRepository tipKorisnikaRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public TipKorisnikaController(ITipKorisnikaRepository tipKorisnikaRepository, IMapper mapper, ILoggerService loggerService)
        {
            this.tipKorisnikaRepository = tipKorisnikaRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve tipove korisnika
        /// </summary>
        /// <returns>Listu tipova korisnika</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<TipKorisnikaDto>> GetTipKorisnikaList()
        {
            List<TipKorisnika> tipKorisnikaList = tipKorisnikaRepository.GetTipKorisnikaList();

            if (tipKorisnikaList == null || tipKorisnikaList.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista tipova korisnika je prazna ili null.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista tipova korisnika je uspešno vraćena!");
            return Ok(mapper.Map<List<TipKorisnikaDto>>(tipKorisnikaList));
        }

        /// <summary>
        /// Vraca tip korisnika za zadatu vrednost id-a
        /// </summary>
        /// <param name="tipKorisnikaId"></param>
        /// <returns>Objekat tipa korisnika</returns>
        [HttpGet("{tipKorisnikaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TipKorisnikaDto> GetTipKorisnikaById(Guid tipKorisnikaId)
        {
            TipKorisnika tipKorisnika = tipKorisnikaRepository.GetTipKorisnikaById(tipKorisnikaId);

            if (tipKorisnika == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Tip korisnika sa tim id-em nije pronađen.");
                return NotFound();
            }

            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Tip korisnika sa zadatim id-em je uspešno vraćen!");
            return Ok(mapper.Map<TipKorisnikaDto>(tipKorisnika));
        }

        /// <summary>
        /// Prikazuje sve moguće tipove zahteva 
        /// </summary>
        /// <returns>Listu mogućih zahteva</returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET");

            loggerService.Log(LogLevel.Information, "GetStatus", "Opcije su uspešno vraćene!");
            return Ok();
        }
    }
}
