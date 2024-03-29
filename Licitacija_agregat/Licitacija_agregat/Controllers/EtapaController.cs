﻿using AutoMapper;
using Licitacija_agregat.Data;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EtapaController : ControllerBase
    {
        private readonly IEtapaRepository etapaRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public EtapaController(IEtapaRepository etapaRepository, LinkGenerator link, IMapper mapper, ILoggerService loggerService)
        {
            this.etapaRepository = etapaRepository;
            this.link = link;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraća sve etape na osnovu prosleđenog filtera
        /// </summary>
        /// <param name="dan">Dan etape</param>
        /// <returns>Listu etapa</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<EtapaDto>> GetEtapas(DateTime dan)
        {
            var etape = etapaRepository.GetEtapas(dan);

            if (etape == null || etape.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista etapa je prazna ili null.");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista etapa je uspešno vraćena!");
            return Ok(mapper.Map<List<EtapaDto>>(etape));
        }

        /// <summary>
        /// Vraća etapu po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="etapaId"></param>
        /// <returns>Objekat etape</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{etapaId}")]
        public ActionResult<EtapaDto> GetEtapaById(Guid etapaId)
        {
            var etapaModel = etapaRepository.GetEtapaById(etapaId);

            if(etapaModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Etapa sa tim id-em nije pronađena.");
                return NotFound();
            }

            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Etapa sa zadatim id-em je uspešno vraćena!");
            return Ok(mapper.Map<EtapaDto>(etapaModel));
        }

        /// <summary>
        /// Kreira novu Etapu
        /// </summary>
        /// <param name="etapa"></param>
        /// <returns>Potvrdu o kreiranoj etapi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove etape
        /// {
        ///     "Dan" : "3085-05-16T05:50:05",
        ///     "BrojEtape" : 4
        /// }
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<EtapaConfirmationDto> CreateEtapa([FromBody] EtapaCreationDto etapa)
        {

            try
            {


                var etapaEntity = mapper.Map<Etapa>(etapa);

                var confirmation = etapaRepository.CreateEtapa(etapaEntity);

                etapaRepository.SaveChanges();

                string location = link.GetPathByAction("GetEtapas", "Etapa", new { etapaId = confirmation.EtapaId });

                loggerService.Log(LogLevel.Information, "PostStatus", "Etapa je uspešno kreirana!");
                return Created(location, mapper.Map<EtapaConfirmationDto>(confirmation));
                  
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Etapa nije kreirana, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred when creating an object");
            }
        }
        /// <summary>
        /// Briše etapu po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="etapaId"></param>
        /// <returns>Prazan payload</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{EtapaId}")]
        public IActionResult DeleteEtapa(Guid etapaId)
        {
            try
            {
                var etapa = etapaRepository.GetEtapaById(etapaId);

                if(etapa == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Etapa sa zadatim id-em nije pronađena!");
                    return NotFound();
                }

                etapaRepository.DeleteEtapa(etapaId);
                etapaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Etapa je uspešno obrisana!");
                return NoContent();

            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Etapa nije obrisana, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Menja vrednosti zadate etape
        /// </summary>
        /// <param name="etapa"></param>
        /// <returns>Potvrdu o izmenjenom objektu</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<EtapaDto> UpdateEtapa(EtapaUpdateDto etapa)
        {

            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldEtapa = etapaRepository.GetEtapaById(etapa.EtapaId);
                if (oldEtapa == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Etapa sa zadatim id-em nije pronađena!");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Etapa etapaEntity = mapper.Map<Etapa>(etapa);

                mapper.Map(etapaEntity, oldEtapa); //Update objekta koji treba da sačuvamo u bazi                

                etapaRepository.SaveChanges(); //Perzistiramo promene
                loggerService.Log(LogLevel.Information, "PutStatus", "Etapa je uspešno izmenjena!");
                return Ok(mapper.Map<EtapaDto>(oldEtapa));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Etapa nije izmenjena, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Prikazuje sve moguće tipove zahteva 
        /// </summary>
        /// <returns>Listu mogućih zahteva</returns>
        [HttpOptions]
        public IActionResult GetEtapaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            loggerService.Log(LogLevel.Information, "GetStatus", "Opcije su uspešno vraćene!");
            return Ok();
        }
    }
}
