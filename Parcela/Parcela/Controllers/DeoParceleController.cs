using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Controllers
{
    /// <summary>
    /// Kontroler za delove parcele
    /// </summary>
    [ApiController]
    [Route("api/deoParcele")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository deoParceleRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor kontrolera za delove parcele
        /// </summary>
        public DeoParceleController(IDeoParceleRepository deoParceleRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.deoParceleRepository = deoParceleRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve delove parcele.
        /// </summary>
        /// <returns>Lista delova parcele</returns>
        /// <response code="200">Vraca listu delova parcele</response>
        /// <response code="404">Nije pronadjen ni jedna deo parcele</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DeoParceleDto>> GetExamRegistrations()
        {
            var deloviParcele = deoParceleRepository.GetDeloveParcele();

            if (deloviParcele == null || deloviParcele.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<DeoParceleDto>>(deloviParcele));
        }

        /// <summary>
        /// Vraća jedan deo parcele osnovu ID-ja.
        /// </summary>
        /// <param name="deoParceleId">ID dela parcele</param>
        /// <returns></returns>
        /// <response code="200">Vraća trazeni deo parcele</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{deoParceleId}")]
        public ActionResult<DeoParceleDto> GetDeoParcele(Guid deoParceleId)
        {
            var deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleId);

            if (deoParcele == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DeoParceleDto>(deoParcele));
        }

        /// <summary>
        /// Kreira novi deo parcele.
        /// </summary>
        /// <param name="deoParcele">Model dela parcele</param>
        /// <returns>Potvrdu o kreiranoj prijavi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dela parcele \
        /// POST /api/deoParcele \
        /// {     \
        ///     DeoParceleId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e5f"), \
        ///     ParcelaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"), \
        ///     PovrsinaDelaParcele = "1000ha", \
        ///     RbrDelaParcele = 1 \
        ///}
        /// </remarks>
        /// <response code="200">Vraca kreirani deo parcele</response>
        /// <response code="500">Doslo je do greske na serveru</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeoParceleConfirmationDto> CreateDeoParcele([FromBody] DeoParceleCreationDto deoParcele)
        {
            try
            {
                DeoParcele deoParceleEntity = mapper.Map<DeoParcele>(deoParcele);
                DeoParceleConfirmation confirmation = deoParceleRepository.CreateDeoParcele(deoParceleEntity);


                var validator = new DeoParceleCreationValidator();
                var results = validator.Validate(deoParcele);

                results.AddToModelState(ModelState, null);


                deoParceleRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetDeoParcele", "DeoParcele", new { deoParceleId = confirmation.DeoParceleId });

                return Created(location, mapper.Map<DeoParceleConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Azurira jedan deo parcele.
        /// </summary>
        /// <param name="deoParcele">Model parcele koji se azurira</param>
        /// <returns>Potvrdu o modifikovanom delu parcele.</returns>
        /// <response code="200">Vraca azurirani deo parcele</response>
        /// <response code="400">Deo parcele koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja dela parcele</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeoParceleDto> UpdateDeoParcele(DeoParceleUpdateDto deoParcele)
        {
            try
            {
                var oldDeoParcele = deoParceleRepository.GetDeoParceleById(deoParcele.DeoParceleId);
                if (oldDeoParcele == null)
                {
                    return NotFound();
                }
                DeoParcele deoParceleEntity = mapper.Map<DeoParcele>(deoParcele);

                mapper.Map(deoParceleEntity, oldDeoParcele);


                var validator = new DeoParceleUpdateValidator();
                var results = validator.Validate(deoParcele);

                results.AddToModelState(ModelState, null);


                deoParceleRepository.SaveChanges();
                return Ok(mapper.Map<DeoParceleDto>(oldDeoParcele));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog dela parcele na osnovu ID-ja.
        /// </summary>
        /// <param name="deoParceleId">ID dela parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Deo parcele uspesno obrisan</response>
        /// <response code="404">Nije pronadjen deo parcele za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja dela parcele</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{deoParceleId}")]
        public IActionResult DeleteDeoParcele(Guid deoParceleId)
        {
            try
            {
                var deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleId);

                if (deoParcele == null)
                {
                    return NotFound();
                }

                deoParceleRepository.DeleteDeoParcele(deoParceleId);
                deoParceleRepository.SaveChanges();
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
