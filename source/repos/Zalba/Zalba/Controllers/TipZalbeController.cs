﻿using System;
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
    /// Kontroler za tipove zalbi
    /// </summary>
    [ApiController]
    [Route("api/tipZalbe")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class TipZalbeController : ControllerBase
    {
        private readonly ITipZalbeRepository tipZalbeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor kontrolera za tipove zalbi
        /// </summary>
        public TipZalbeController(ITipZalbeRepository tipZalbeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tipZalbeRepository = tipZalbeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve tipove zalbi.
        /// </summary>
        /// <returns>Lista tipova zalbi</returns>
        /// <response code="200">Vraca listu tipova zalbi</response>
        /// <response code="404">Nije pronadjen ni jedna tip zalbe</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<TipZalbeDto>> GetExamRegistrations()
        {
            var tipoviZalbi = tipZalbeRepository.GetTipoveZalbi();

            if (tipoviZalbi == null || tipoviZalbi.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<TipZalbeDto>>(tipoviZalbi));
        }

        /// <summary>
        /// Vraća jedan tip zalbe osnovu ID-ja.
        /// </summary>
        /// <param name="tipZalbeId">ID tipa zalbe</param>
        /// <returns></returns>
        /// <response code="200">Vraća trazeni tip zalbe</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{tipZalbeId}")]
        public ActionResult<TipZalbeDto> GetTipZalbe(Guid tipZalbeId)
        {
            var tipZalbe = tipZalbeRepository.GetTipZalbeById(tipZalbeId);

            if (tipZalbe == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TipZalbeDto>(tipZalbe));
        }

        /// <summary>
        /// Kreira novi tip zalbe.
        /// </summary>
        /// <param name="tipZalbe">Model tipa zalbe</param>
        /// <returns>Potvrdu o kreiranoj prijavi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog tipa zalbe \
        /// POST /api/tipZalbe \
        /// {     \
        ///     "TipZalbeId": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "NazivTipa": "Žalba na tok javnog nadmetanaja", \
        ///}
        /// </remarks>
        /// <response code="200">Vraca kreirani tip zalbe</response>
        /// <response code="500">Doslo je do greske na serveru</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipZalbeConfirmationDto> CreateTipZalbe([FromBody] TipZalbeCreationDto tipZalbe)
        {
            try
            {
                TipZalbe tipZalbeEntity = mapper.Map<TipZalbe>(tipZalbe);
                TipZalbeConfirmation confirmation = tipZalbeRepository.CreateTipZalbe(tipZalbeEntity);


                var validator = new TipZalbeCreationValidator();
                var results = validator.Validate(tipZalbe);

                results.AddToModelState(ModelState, null);


                tipZalbeRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetTipZalbe", "TipZalbe", new { tipZalbeId = confirmation.TipZalbeId });

                return Created(location, mapper.Map<TipZalbeConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Azurira jedan tip zalbe.
        /// </summary>
        /// <param name="tipZalbe">Model zalbe koji se azurira</param>
        /// <returns>Potvrdu o modifikovanom tipu zalbe.</returns>
        /// <response code="200">Vraca azurirani tip zalbe</response>
        /// <response code="400">Tip zalbe koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja tipa zalbe</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipZalbeDto> UpdateTipZalbe(TipZalbeUpdateDto tipZalbe)
        {
            try
            {
                var oldTipZalbe = tipZalbeRepository.GetTipZalbeById(tipZalbe.TipZalbeId);
                if (oldTipZalbe == null)
                {
                    return NotFound();
                }
                TipZalbe tipZalbeEntity = mapper.Map<TipZalbe>(tipZalbe);

                mapper.Map(tipZalbeEntity, oldTipZalbe);


                var validator = new TipZalbeUpdateValidator();
                var results = validator.Validate(tipZalbe);

                results.AddToModelState(ModelState, null);


                tipZalbeRepository.SaveChanges();
                return Ok(mapper.Map<TipZalbeDto>(oldTipZalbe));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog tipa zalbe na osnovu ID-ja.
        /// </summary>
        /// <param name="tipZalbeId">ID tipa zalbe</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip zalbe uspesno obrisan</response>
        /// <response code="404">Nije pronadjen tip zalbe za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja tipa zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{tipZalbeId}")]
        public IActionResult DeleteTipZalbe(Guid tipZalbeId)
        {
            try
            {
                var tipZalbe = tipZalbeRepository.GetTipZalbeById(tipZalbeId);

                if (tipZalbe == null)
                {
                    return NotFound();
                }

                tipZalbeRepository.DeleteTipZalbe(tipZalbeId);
                tipZalbeRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa tipovima zalbi
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