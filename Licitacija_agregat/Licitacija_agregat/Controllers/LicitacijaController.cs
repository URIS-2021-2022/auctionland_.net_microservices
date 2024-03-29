﻿using AutoMapper;
using Licitacija_agregat.Data;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using Licitacija_agregat.ServiceCalls;
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
    [Authorize]
    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IProgramRepository programRepository;

        public LicitacijaController(ILicitacijaRepository licitacijaRepository, LinkGenerator link, IMapper mapper, ILoggerService loggerService, IProgramRepository programRepository)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.link = link;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.programRepository = programRepository;
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
        public ActionResult<List<LicitacijaDto>> GetLicitacijas(DateTime datum)
        {

            List<Licitacija> licitacijaList = licitacijaRepository.GetLicitacijas();

            var licitacije = licitacijaRepository.GetLicitacijas(datum);
            if (licitacije == null || licitacije.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista licitacija je prazna ili null.");
                return NoContent();
            }

            List<LicitacijaDto> licitacijaDtoList = mapper.Map<List<LicitacijaDto>>(licitacijaList);


            foreach (LicitacijaDto lDto in licitacijaDtoList)
            {
                lDto.Program = programRepository.GetProgramByIdAsync(lDto.ProgramId, Request).Result;
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista licitacija je uspešno vraćena!");
            return Ok(mapper.Map<List<LicitacijaDto>>(licitacije));
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
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Licitacija sa tim id-em nije pronađena.");
                return NotFound();
            }

            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Licitacija sa zadatim id-em je uspešno vraćena!");
            return Ok(mapper.Map<LicitacijaDto>(licitacijaModel));
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
        public ActionResult<LicitacijaConfirmationDto> CreateLicitacija([FromBody] LicitacijaCreationDto licitacija)
        {
            try
            {
                var licitacijaEntity = mapper.Map<Licitacija>(licitacija);

                var confirmation = licitacijaRepository.CreateLicitacija(licitacijaEntity);
                licitacijaRepository.SaveChanges();
                string location = link.GetPathByAction("GetLicitacijas", "Licitacija", new { licitacijaId = confirmation.LicitacijaId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Licitacija je uspešno kreirana!");
                return Created(location, mapper.Map<LicitacijaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Licitacija nije kreirana, došlo je do greške!");
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
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Licitacija sa zadatim id-em nije pronađena!");
                    return NotFound();
                }

                licitacijaRepository.DeleteLicitacija(licitacijaId);
                licitacijaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Licitacija je uspešno obrisana!");
                return NoContent();

            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Licitacija nije obrisana, došlo je do greške!");
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
        public ActionResult<LicitacijaDto> UpdateLicitacija(LicitacijaUpdateDto licitacija)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldLicitacija = licitacijaRepository.GetLicitacijaById(licitacija.LicitacijaId);
                if (oldLicitacija == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Licitacija sa zadatim id-em nije pronađena!");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Licitacija licitacijaEntity = mapper.Map<Licitacija>(licitacija);

                mapper.Map(licitacijaEntity, oldLicitacija); //Update objekta koji treba da sačuvamo u bazi                

                licitacijaRepository.SaveChanges(); //Perzistiramo promene
                loggerService.Log(LogLevel.Information, "PutStatus", "Licitacija je uspešno izmenjena!");
                return Ok(mapper.Map<LicitacijaDto>(oldLicitacija));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Licitacija nije izmenjena, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
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
            loggerService.Log(LogLevel.Information, "GetStatus", "Opcije su uspešno vraćene!");
            return Ok();
        }


    }
}
