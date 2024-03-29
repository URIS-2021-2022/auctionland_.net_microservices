﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Oglas_Agregat.Data;
using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using Oglas_Agregat.ServiceCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Controllers
{
    /// <summary>
    /// Predstavlja kontroler oglasa
    /// </summary>
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Route("api/Oglasi")]
    public class OglasController : ControllerBase //ControllerBase za neke dodatne funkcionalnosti npr manipulacija status kodovima 
    {
        private readonly IOglasRepository oglasRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IJavnoNadmetanjeService JavnoNadmetanjeService;


        public OglasController(IOglasRepository oglasRepository, LinkGenerator link, IMapper mapper, 
            ILoggerService loggerService, IJavnoNadmetanjeService javnoNadmetanjeService) //kad se kreira objekat kontrolera mora da se prosledi nesto sto implementira ovaj interfejs, kod mene oglasConfirmation
        {
            this.oglasRepository = oglasRepository;
            this.link = link;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.JavnoNadmetanjeService = javnoNadmetanjeService;

        }

        /// <summary>
        /// Vraća sve oglase na osnovu prosleđenog filtera
        /// </summary>
        /// <param name="DatumObjave">Datum objave oglasa</param>
        /// <returns>Listu oglasa</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<OglasDto>> GetOglasi(DateTime DatumObjave)
        {
            //treba da zavisimo od apstrakcije a ne od konkretne implementacije, ne treba da pravimo instancu klase i direktno pozivamo metodu sto nije dobro, zato smo pravili interfejs
            List<Oglas> oglasi = oglasRepository.GetOglasi(DatumObjave);
            if (oglasi == null || oglasi.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista oglasa je prazna ili null.");
                return NoContent();
            }

            List<OglasDto> oglasDtoList = mapper.Map<List<OglasDto>>(oglasi);

            foreach (OglasDto odto in oglasDtoList)
            {
                
                odto.JavnoNadmetanje = JavnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(odto.JavnoNadmetanjeId, Request).Result;
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista oglasa je uspesno vracena!");
            return Ok(mapper.Map<List<OglasDto>>(oglasi));
        }

        /// <summary>
        /// Vraća oglas po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="oglasId"></param>
        /// <returns>Objekat oglasa</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{oglasId}")]
        public ActionResult<OglasDto> GetOglasById(Guid oglasId)
        {
            var oglas = oglasRepository.GetOglasById(oglasId);

            if (oglas == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Oglas sa datim id-em nije pronadjen.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Oglas sa datim id-em je uspesno vracen!");
            return Ok(mapper.Map<OglasConfirmationDto>(oglas));
        }

        /// <summary>
        /// Kreira novi oglas
        /// </summary>
        /// <param name="oglas"></param>
        /// <returns>Potvrdu o kreiranom oglasu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog oglasa
        /// {
        ///     "DatumObjave" : "2021-05-16T05:50:06",
        ///     "RokZaZalbu" : "2021-06-16T05:50:06",
        ///     "OpisOglasa" : "opis1"
        ///     "ObjavljenUListu" : "00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"
        /// }
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<OglasConfirmationDto> CreateOglas([FromBody] OglasCreateDto oglas)
        {
            try
            {
                var oglasEntity = mapper.Map<Oglas>(oglas);

                var confirmation = oglasRepository.CreateOglas(oglasEntity);

                oglasRepository.SaveChanges();

                string location = link.GetPathByAction("GetOglasi", "Oglas", new { oglasId = confirmation.OglasId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Oglas je uspesno napravljen!");
                return Created(location, mapper.Map<OglasConfirmationDto>(confirmation));

            }
            catch (Exception) {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Oglas nije kreiran, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while creating the object.");

            }
        }

        /// <summary>
        /// Briše oglas po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="oglasId"></param>
        /// <returns>Prazan payload</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{oglasId}")]
        public IActionResult DeleteOglas(Guid oglasId) //moze I jer nema povratni tip
        {
            try
            {
                var oglas = oglasRepository.GetOglasById(oglasId);

                if (oglas == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Oglas sa datim id-em nije pronadjen.");
                    return NotFound();
                }

                oglasRepository.DeleteOglas(oglasId);
                oglasRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Oglas je uspesno obrisan!");
                return NoContent(); //e sve je okej proslo ali ja ne vracam nikakav sadrzaj - 200

            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Oglas nije obrisan, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while deleting the object.");
            }
        }

        /// <summary>
        /// Menja vrednosti postojećeg oglasa
        /// </summary>
        /// <param name="oglas"></param>
        /// <returns>Potvrdu o izmenjenom objektu</returns>
        /// Primer zahteva za izmenu postojećeg oglasa
        /// <remarks>
        /// {        
        ///     "OglasId": "00f78e6b-a2bb-43b5-b3bb-f5708d1a5129",
        ///     "DatumObjave" : "2021-05-16T05:50:06",
        ///     "RokZaZalbu" : "2021-06-16T05:50:06",
        ///     "OpisOglasa" : "izmena",
        ///     "ObjavljenUListu" : "00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"
        /// }
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        [HttpPut]
        public ActionResult<OglasDto> UpdateOglas(OglasUpdateDto oglas)
        {

            try
            {
                //Proveriti da li uopšte postoji oglas koju pokušavamo da ažuriramo.
                var oldOglas = oglasRepository.GetOglasById(oglas.OglasId);
                if (oldOglas == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Oglas sa datim id-em nije pronadjen.");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Oglas oglasEntity = mapper.Map<Oglas>(oglas);

                mapper.Map(oglasEntity, oldOglas); //Update objekta koji treba da sačuvamo u bazi                

                oglasRepository.SaveChanges(); //Perzistiramo promene
                loggerService.Log(LogLevel.Information, "PutStatus", "Oglas je uspesno izmenjen!");
                return Ok(mapper.Map<OglasDto>(oldOglas));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Oglas nije izmenjen, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }


        /// <summary>
        /// Prikazuje sve moguće tipove zahteva 
        /// </summary>
        /// <returns>Listu mogućih zahteva</returns>
        [HttpOptions]
        public IActionResult GetOglasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }



    }
}
