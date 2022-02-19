using AutoMapper;
using Licitacija_agregat.Data;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]

    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;

        public LicitacijaController(ILicitacijaRepository licitacijaRepository, LinkGenerator link, IMapper mapper)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.link = link;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve licitacije na osnovu prosleđenog parametra
        /// </summary>
        /// <param name="datum"></param>
        /// <returns>Listu licitacija</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Licitacija>> GetLicitacijas(DateTime datum)
        {
            var licitacije = licitacijaRepository.GetLicitacijas(datum);
            if (licitacije == null || licitacije.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<Licitacija>>(licitacije));
        }
        /// <summary>
        /// Vraća licitaciju po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="licitacijaId"></param>
        /// <returns>Objekat licitacije</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{licitacijaId}")]
        public ActionResult<LicitacijaDto> GetLicitacijaById(Guid licitacijaId)
        {
            var licitacijaModel = licitacijaRepository.GetLicitacijaById(licitacijaId);

            if(licitacijaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<LicitacijaDto>>(licitacijaModel));
        }

        /// <summary>
        /// Kreira novu Licitaciju
        /// </summary>
        /// <param name="licitacija"></param>
        /// <returns>Potvrdu o kreiranoj licitaciji</returns>
        /// <remarks> Primer zahteva za kreiranje nove licitacije
        /// {
        ///     "Broj" : 5,
        ///     "Godina" : 23,
        ///     "Datum" : "2066-05-16T05:50:06",
        ///     "Ogranicenje" : 234,
        ///     "Korak_cene" : 1235,
        ///     "Lista_dokumentacije_fizicka_lica" : ["Vlado", "Kika", "Cile"],
        ///     "Lista_dokumentacije_pravna_lica" : ["Malena", "Flemi", "Djole"],
        ///     "JavnoNadmetanje" : ["Subotica"],
        ///     "Rok_za_dostavljanje_prijave" : "2005-06-16T05:50:06"
        /// }
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<LicitacijaDto> CreateLicitacija([FromBody] LicitacijaCreationDto licitacija)
        {
            try
            {
                var licitacijaEntity = mapper.Map<Licitacija>(licitacija);

                var confirmation = licitacijaRepository.CreateLicitacija(licitacijaEntity);
                licitacijaRepository.SaveChanges();
                string location = link.GetPathByAction("GetLicitacijas", "Licitacija", new { licitacijaId = confirmation.LicitacijaId });
                return Created(location, mapper.Map<LicitacijaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }
        }

        /// <summary>
        /// Briše licitaciju po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="licitacijaId"></param>
        /// <returns>Prazan payload</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{LicitacijaId}")]
        public IActionResult DeleteLicitacija(Guid licitacijaId)
        {
            try
            {
                var licitacija = licitacijaRepository.GetLicitacijaById(licitacijaId);

                if(licitacija == null)
                {
                    return NotFound();
                }

                licitacijaRepository.DeleteLicitacija(licitacijaId);
                licitacijaRepository.SaveChanges();
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        /// <summary>
        /// Menja vrednost zadate licitacije
        /// </summary>
        /// <param name="licitacija"></param>
        /// <returns>Potvrdu o izmenjenom objektu</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<LicitacijaConfirmationDto> UpdateLicitacija([FromBody] LicitacijaUpdateDto licitacija)
        {
            try
            {
                var confirmation = licitacijaRepository.GetLicitacijaById(licitacija.LicitacijaId);

                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (licitacijaRepository.GetLicitacijaById(licitacija.LicitacijaId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Licitacija licitacijaEntity = mapper.Map<Licitacija>(licitacija);
                licitacijaRepository.SaveChanges();
                return Ok(mapper.Map<LicitacijaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }

        /// <summary>
        /// Prikazuje sve moguće tipove zahteva 
        /// </summary>
        /// <returns>Listu mogućih zahteva</returns>
        [HttpOptions]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
