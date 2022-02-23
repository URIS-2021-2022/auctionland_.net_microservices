using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.Liciter;
using Liciter___Agregat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Controllers
{
    [Route("api/liciter")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public  class LiciterController : ControllerBase
    {
        private readonly ILiciterRepository liciterRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public LiciterController(ILiciterRepository liciterRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.liciterRepository = liciterRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca licitere iz liste
        /// </summary>
        /// <param name="JMBG_MaticniBroj"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<LiciterDto>> GetLiciteri(string JMBG_MaticniBroj)
        {
            List<LiciterModel> liciteri = liciterRepository.GetLiciters(/*JMBG_MaticniBroj*/);
            if (liciteri == null || liciteri.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista licitera je prazna ili null");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista licitera je uspesno vracena!");
            return Ok(mapper.Map<List<LiciterDto>>(liciteri));
        }

        /// <summary>
        /// Vraca licitera na osnovu id-a kojeg mu prosledimo
        /// </summary>
        /// <param name="liciterId"></param>
        /// <returns></returns>
        /// 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{liciterId}")]
        public ActionResult<LiciterDto> GetLiciterbyId(Guid liciterId)
        {
            LiciterModel liciterModel = liciterRepository.GetLiciterById(liciterId);
            if (liciterModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Liciter sa tim id-em nije pronadjeno");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Liciter sa zadatim id-em je uspesno vracena!");
            return Ok(mapper.Map<LiciterDto>(liciterModel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="liciter"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LiciterConfirmationDto> CreateLiciter([FromBody] LiciterCreationDto liciter)
        {
            try
            {

                LiciterModel liciter2 = mapper.Map<LiciterModel>(liciter);
                LiciterConfirmation confirmation = liciterRepository.CreateLiciter(liciter2);
                liciterRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetLiciteri", "Liciter", new { liciterId = confirmation.LiciterId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Liciter je uspesno napravljen!");
                return Created(location, mapper.Map<LiciterConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Liciter nije kreiran, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error " + ex.Message);
            }

        }

        /// <summary>
        /// Briše licitera sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="liciterId"></param>
        /// <returns>Potvrdu o izbrisanom liciteru</returns>
        ///  <response code="200">Vraća izbrisanog licitera</response>
        /// <response code="400">Liciter koji se brise nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja Licitera</response>
        [HttpDelete("{liciterId}")]
        public IActionResult DeleteLiciter(Guid liciterId)
        {
            try
            {
                LiciterModel liciterModel = liciterRepository.GetLiciterById(liciterId);
                if (liciterModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Liciter sa tim id-em nije pronadjen");
                    return NotFound();
                }
                liciterRepository.DeleteLiciter(liciterId);
                liciterRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Liciter je uspesno obrisan!");
                return NoContent();
            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Doslo je do greske prilikom brisanja licitera!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        /// <summary>
        /// Vrši izmenu nad liciterom koji se prosledio u body-u
        /// </summary>
        /// <param name="liciter"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Liciter uspešno ažuriran</response>
        /// <response code="404">Nije pronađen liciter za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja licitera</response>
        public ActionResult<LiciterConfirmationDto> UpdateLiciter(LiciterUpdateDto liciter)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (liciterRepository.GetLiciterById(liciter.LiciterId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Liciter sa tim id-em nije pronadjen");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                LiciterModel liciterModel = mapper.Map<LiciterModel>(liciter);
                LiciterConfirmation confirmation = liciterRepository.UpdateLiciter(liciterModel);
                liciterRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Liciter je uspesno izmenjen!");
                return Ok(mapper.Map<LiciterConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Doslo je do greske prilikom azuriranja licitera!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
