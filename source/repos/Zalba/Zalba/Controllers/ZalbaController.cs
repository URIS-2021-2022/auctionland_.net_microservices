using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Zalba.Data;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Controllers
{
    /// <summary>
    /// Kontroler za zalbe
    /// </summary>
    [ApiController]
    [Route("api/zalba")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class ZalbaController : ControllerBase
    {
        private readonly IZalbaRepository zalbaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor kontrolera za zalbe
        /// </summary>
        public ZalbaController(IZalbaRepository zalbaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.zalbaRepository = zalbaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve zalbe za zadate filtere.
        /// </summary>
        /// <param name="statusZalbe">Status zalbe (npr. otvorena)</param>
        /// <returns>Lista zalbi</returns>
        /// <response code="200">Vraća listu zalbi</response>
        /// <response code="404">Nije pronađena ni jedna zalba</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ZalbaDto>> GetZalbe(string statusZalbe)
        {
            var zalbe = zalbaRepository.GetZalbe(statusZalbe);

            if (zalbe == null || zalbe.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ZalbaDto>>(zalbe));
        }

        /// <summary>
        /// Vraca jednu zalbu na osnovu ID-ja zalbe.
        /// </summary>
        /// <param name="zalbaId">ID zalbe</param>
        /// <returns></returns>
        /// <response code="200">Vraca trazenu zalbu</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{zalbaId}")]
        public ActionResult<ZalbaDto> GetZalba(Guid zalbaId)
        {
            var zalba = zalbaRepository.GetZalbaById(zalbaId);

            if (zalba == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ZalbaDto>(zalba));
        }

        /// <summary>
        /// Kreira novu prijavu zalbu.
        /// </summary>
        /// <param name="zalba">Model zalbe</param>
        /// <returns>Potvrdu o kreiranoj zalbi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove zalbe \
        /// POST /api/zalba \
        /// {     \
        ///     "ZalbaId" = "6a411c13-a195-48f7-8dbd-67596c3974c0", \
        ///     Tip = "044f3de0-a9dd-4c2e-b745-89976a1b2a36", \
        ///     "DatumPodnosenjaZalbe" = "2020-11-15T09:00:00", \
        ///     "PodnosilacZalbe" = "6a411c13-a195-48f7-8dbd-67596c3974c4", \
        ///     "RazlogZalbe" = "razlog", \
        ///     "Obrazlozenje" = "obrazlozenje", \
        ///     "DatumResenja" = "2022-02-20T09:00:00", \
        ///     "BrojResenja" = "S123", \
        ///     "StatusZalbe" = "Otvorena", \
        ///     "BrojOdluke" = "S001", \
        ///     "RadnjaNaOsnovuZalbe" = "JN ne ide u drugi krug" \
        ///}
        /// </remarks>
        /// <response code="200">Vraca kreiranu zalbu</response>
        /// <response code="500">Doslo je do greske na serveru</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZalbaConfirmationDto> CreateZalba([FromBody] ZalbaCreationDto zalba)
        {
            try
            {
                ZalbaM zalbaEntity = mapper.Map<ZalbaM>(zalba);
                ZalbaConfirmation confirmation = zalbaRepository.CreateZalba(zalbaEntity);


                var validator = new ZalbaCreationValidator();
                var results = validator.Validate(zalba);

                results.AddToModelState(ModelState, null);


                zalbaRepository.SaveChanges(); 

                string location = linkGenerator.GetPathByAction("GetZalba", "Zalba", new { zalbaId = confirmation.ZalbaId });

                return Created(location, mapper.Map<ZalbaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Azurira jednu zalbu.
        /// </summary>
        /// <param name="zalba">Model zalbe koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj zalbi.</returns>
        /// <response code="200">Vraca azuriranu zalbu</response>
        /// <response code="400">Zalba koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja zalbe</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZalbaDto> UpdateZalba(ZalbaUpdateDto zalba)
        {
            try
            {
                var oldZalba = zalbaRepository.GetZalbaById(zalba.ZalbaId);
                if (oldZalba == null)
                {
                    return NotFound();
                }
                ZalbaM zalbaEntity = mapper.Map<ZalbaM>(zalba);

                mapper.Map(zalbaEntity, oldZalba);

                var validator = new ZalbaUpdateValidator();
                var results = validator.Validate(zalba);

                results.AddToModelState(ModelState, null);

                zalbaRepository.SaveChanges();
                return Ok(mapper.Map<ZalbaDto>(oldZalba));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne zalbe na osnovu ID-ja zalbe.
        /// </summary>
        /// <param name="zalbaId">ID zalbe</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Zalba uspesno obrisana</response>
        /// <response code="404">Nije pronadjena zalba za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{zalbaId}")]
        public IActionResult DeleteZalba(Guid zalbaId)
        {
            try
            {
                var zalba = zalbaRepository.GetZalbaById(zalbaId);

                if (zalba == null)
                {
                    return NotFound();
                }

                zalbaRepository.DeleteZalba(zalbaId);
                zalbaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa zalbama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        //[AllowAnonymous]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
