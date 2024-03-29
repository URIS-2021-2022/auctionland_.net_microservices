﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using OdlukaODavanjuUZakup.Data;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Controllers
{
    /// <summary>
    /// Predstavlja tipove garancija placanja, mogu biti : Jemstvo, bankarska garancija, garancija nekretninom, zirantska, uplata gotovinom
    /// </summary>
    [ApiController]
    [Route("api/garantPlacanja")]
    [Produces("application/json", "application/xml")]
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GarantPlacanjaController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IGarantPlacanjaRepository garantPlacanjaRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        /// <summary>
        /// Konstruktor 
        /// </summary>
        /// <param name="garantPlacanjaRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        public GarantPlacanjaController(IGarantPlacanjaRepository garantPlacanjaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService) 
        {
            this.linkGenerator = linkGenerator;
            this.garantPlacanjaRepository = garantPlacanjaRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }
        /// <summary>
        /// Vraca listu svih garanta placanja
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Uspesno vraca sve</response>
        /// <response code="404">Nema garanta placanja</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<GarantPlacanjaDto>> GetGaranti()
        {
            var garanti = garantPlacanjaRepository.GetGarantiPlacanja();
            if (garanti == null || garanti.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista garanta je prazna ili null.");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista garanta je uspešno vraćena!");
            return Ok(mapper.Map<List<GarantPlacanjaDto>>(garanti));
            

        }
        /// <summary>
        /// Vraca garant sa trazenim ID
        /// </summary>
        /// <param name="garantPlacanjaID">Sifra garanta placanja</param>
        /// <returns></returns>
        ///     <response code="200">Uspesno vraca pronadjenog</response>
        ///     <response code="404">Ne postoji takav u bazi</response>
        [HttpGet("{GarantPlacanjaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GarantPlacanjaDto> GetGarant(Guid garantPlacanjaID)
        {
            var garant = garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanjaID);
            if (garant == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Garant sa tim id-em nije pronađen.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Garant sa zadatim id-em je uspešno vraćen!");
            return Ok(mapper.Map<GarantPlacanjaDto>(garant));
        }
        /// <summary>
        /// Upis novog garanta placanja u bazu
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Vraca kreiran garant </response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<GarantPlacanjaConfirmationDto> CreateGarantPlacanja([FromBody] GarantPlacanjaCreationDto garantPlacanja)
        {
            try
            {
                GarantPlacanja garantPlacanjaEntity = mapper.Map<GarantPlacanja>(garantPlacanja);
                GarantPlacanjaConfirmation confirmation = garantPlacanjaRepository.CreateGarantPlacanja(garantPlacanjaEntity);

                garantPlacanjaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetGaranti", "GarantPlacanja", new { GarantPlacanjaID = confirmation.GarantPlacanjaID });
                loggerService.Log(LogLevel.Information, "PostStatus", "Garant je uspešno kreiran!");
                return Created(location, mapper.Map<GarantPlacanjaConfirmationDto>(confirmation));
            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Garant nije kreiran, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        /// <summary>
        /// Brisemo garant iz baze
        /// </summary>
        /// <param name="garantPlacanjaID">Prosledjeni ID po kom brisem</param>
        /// <returns></returns>
        /// <response code="200">Vraca izbrisani garant</response>
        /// <response code="404">Garant nije pronadjeno</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja</response>
        /// <response code ="204" >Nakon uspesnog brisanja</response>
        [HttpDelete("{GarantPlacanjaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteGarant(Guid garantPlacanjaID)
        {
            try
            {
                var garant = garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanjaID);
                if (garant == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Garant sa zadatim id-em nije pronađen!");
                    return NotFound();
                }
                garantPlacanjaRepository.DeleteGarantPlacanja(garantPlacanjaID);
                garantPlacanjaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Garant je uspešno obrisan!");
                return NoContent();
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Garant nije obrisan, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        /// <summary>
        /// Azuriranje garanta
        /// </summary>
        /// <param name="garantPlacanja"></param>
        /// <returns></returns>
        /// <response code="200">Vraca azuriran garant</response>
        /// <response code="404">Garant nije pronadjeno</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<GarantPlacanjaConfirmationDto> UpdateGarantPlacanja(GarantPlacanjaUpdateDto garantPlacanja)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanja.GarantPlacanjaID) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Garant sa zadatim id-em nije pronađen!");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                GarantPlacanja garantPlacanja2 = mapper.Map<GarantPlacanja>(garantPlacanja);
                GarantPlacanjaConfirmation confirmation = garantPlacanjaRepository.UpdateGarantPlacanja(garantPlacanja2);
                garantPlacanjaRepository.SaveChanges();

                loggerService.Log(LogLevel.Information, "PutStatus", "Garant je uspešno izmenjen!");
                return Ok(mapper.Map<GarantPlacanjaDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Garant nije izmenjen, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        [HttpOptions]
        public IActionResult GetGarantPlacanjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            loggerService.Log(LogLevel.Information, "GetStatus", "Opcije su uspešno vraćene!");
            return Ok();
        }
    }
    }
