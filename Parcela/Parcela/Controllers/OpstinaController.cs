using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Controllers
{
    /// <summary>
    /// Kontroler za opstine
    /// </summary>
    [ApiController]
    [Route("api/opstina")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class OpstinaController : ControllerBase
    {
        private readonly IOpstinaRepository opstinaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        /// <summary>
        /// Konstruktor kontrolera za opstine
        /// </summary>
        public OpstinaController(IOpstinaRepository opstinaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.opstinaRepository = opstinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve opstine.
        /// </summary>
        /// <returns>Lista opstina</returns>
        /// <response code="200">Vraca listu opstina</response>
        /// <response code="404">Nije pronadjen ni jedna opstina</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OpstinaDto>> GetExamRegistrations()
        {
            var opstine = opstinaRepository.GetOpstine();

            if (opstine == null || opstine.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista opstina je prazna ili null");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista opstina je uspesno vracena!");
            return Ok(mapper.Map<List<OpstinaDto>>(opstine));
        }

        /// <summary>
        /// Vraća jedan opstina osnovu ID-ja.
        /// </summary>
        /// <param name="opstinaId">ID dela parcele</param>
        /// <returns></returns>
        /// <response code="200">Vraća trazeni opstina</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{opstinaId}")]
        public ActionResult<OpstinaDto> GetOpstina(Guid opstinaId)
        {
            var opstina = opstinaRepository.GetOpstinaById(opstinaId);

            if (opstina == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Opstina sa tim id-em nije pronadjena");
                return NotFound();
            }

            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Opstina sa zadatim id-em je uspesno vracena!");
            return Ok(mapper.Map<OpstinaDto>(opstina));
        }

        /// <summary>
        /// Kreira novi opstina.
        /// </summary>
        /// <param name="opstina">Model dela parcele</param>
        /// <returns>Potvrdu o kreiranoj prijavi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dela parcele \
        /// POST /api/opstina \
        /// {     \
        ///     OpstinaId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"), \
        ///     NazivOpstine = "Bikovo" \
        ///}
        /// </remarks>
        /// <response code="200">Vraca kreirani opstina</response>
        /// <response code="500">Doslo je do greske na serveru</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OpstinaConfirmationDto> CreateOpstina([FromBody] OpstinaCreationDto opstina)
        {
            try
            {
                Opstina opstinaEntity = mapper.Map<Opstina>(opstina);
                OpstinaConfirmation confirmation = opstinaRepository.CreateOpstina(opstinaEntity);


                var validator = new OpstinaCreationValidator();
                var results = validator.Validate(opstina);

                results.AddToModelState(ModelState, null);


                opstinaRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetOpstina", "Opstina", new { opstinaId = confirmation.OpstinaId });

                loggerService.Log(LogLevel.Information, "PostStatus", "Opstina je uspesno napravljena!");
                return Created(location, mapper.Map<OpstinaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Opstina nije kreirana, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Azurira jedan opstina.
        /// </summary>
        /// <param name="opstina">Model parcele koji se azurira</param>
        /// <returns>Potvrdu o modifikovanom delu parcele.</returns>
        /// <response code="200">Vraca azurirani opstina</response>
        /// <response code="400">Deo parcele koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja dela parcele</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OpstinaDto> UpdateOpstina(OpstinaUpdateDto opstina)
        {
            try
            {
                var oldOpstina = opstinaRepository.GetOpstinaById(opstina.OpstinaId);
                if (oldOpstina == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Opstina sa tim id-em nije pronadjena");
                    return NotFound();
                }
                Opstina opstinaEntity = mapper.Map<Opstina>(opstina);

                mapper.Map(opstinaEntity, oldOpstina);


                var validator = new OpstinaUpdateValidator();
                var results = validator.Validate(opstina);

                results.AddToModelState(ModelState, null);


                opstinaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Opstina je uspesno izmenjena!");
                return Ok(mapper.Map<OpstinaDto>(oldOpstina));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene opstine");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog dela parcele na osnovu ID-ja.
        /// </summary>
        /// <param name="opstinaId">ID dela parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Deo parcele uspesno obrisan</response>
        /// <response code="404">Nije pronadjen opstina za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja dela parcele</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{opstinaId}")]
        public IActionResult DeleteOpstina(Guid opstinaId)
        {
            try
            {
                var opstina = opstinaRepository.GetOpstinaById(opstinaId);

                if (opstina == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Opstina sa tim id-em nije pronadjena");
                    return NotFound();
                }

                opstinaRepository.DeleteOpstina(opstinaId);
                opstinaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Opstina je uspesno obrisana!");
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa delovima parcele
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
