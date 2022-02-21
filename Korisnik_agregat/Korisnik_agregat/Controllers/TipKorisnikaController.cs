using AutoMapper;
using Korisnik_agregat.Data;
using Korisnik_agregat.Entities;
using Korisnik_agregat.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TipKorisnikaController : ControllerBase
    {
        private readonly ITipKorisnikaRepository tipKorisnikaRepository;
        private readonly IMapper mapper;

        public TipKorisnikaController(ITipKorisnikaRepository tipKorisnikaRepository, IMapper mapper)
        {
            this.tipKorisnikaRepository = tipKorisnikaRepository;
            this.mapper = mapper;
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
                return NoContent();
            }

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
                return NotFound();
            }

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

            return Ok();
        }
    }
}
