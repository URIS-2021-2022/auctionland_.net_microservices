using AutoMapper;
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
    /// Kontroler za uplatu zakupnine
    /// </summary>
    [ApiController]
    [Route("api/zakupnine")]
    [Produces("application/json", "application/xml")]
    public class UplataZakupnineController : ControllerBase
    {
        private readonly ILoggerService loggerService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IUplataZakupnineRepository uplataZakupnineRepository;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uplataZakupnineRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        public UplataZakupnineController(IUplataZakupnineRepository uplataZakupnineRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)

        {
            this.loggerService = loggerService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.uplataZakupnineRepository = uplataZakupnineRepository;
        }
        /// <summary>
        /// Vraća sve uplate zakupnina na osnovu broja računa
        /// </summary>
        /// <param name="broj_racuna">Broj računa na koji se uplata vrši</param>
        /// <returns>Listu uplata zakupnina</returns>
        /// <response code="200">Uspesno vraca sve uplate</response>
        /// <response code="404">Nema uplata u bazi</response>
        [HttpHead]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UplataZakupnineDto>> GetUplate(string broj_racuna)
        {
            var uplate = uplataZakupnineRepository.GetUplateZakupnine(broj_racuna);

            if (uplate == null || uplate.Count == 0)
            {

                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista uplata je prazna ili null.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista uplata je uspešno vraćena!");
            return Ok(mapper.Map<List<UplataZakupnineDto>>(uplate));
        }
        /// <summary>
        /// Vraca jednu uplatu sa prosledjenim ID
        /// </summary>
        /// <param name="uplataZakupnineID">prosledjeni ID uplate koju trazimo</param>
        /// <returns></returns>
        /// <response code="200">Uspesno vraca pronadjenu uplatu</response>
        /// <response code="404">Nema uplate sa tim ID u bazi</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{UplataZakupnineID}")]
        public ActionResult<UplataZakupnineDto> GetUplata(Guid uplataZakupnineID)
        {
            var uplata = uplataZakupnineRepository.GetUplataZakupnineById(uplataZakupnineID);

            if (uplata == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Uplata sa tim id-em nije pronađena.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Uplata sa zadatim id-em je uspešno vraćena!");
            return Ok(mapper.Map<UplataZakupnineDto>(uplata));
        }
        /// <summary>
        /// Kreiranje uplate zakupnine u bazi
        /// <remarks>
        /// </remarks>
        /// </summary>
        /// <param name="uplataZakupnine">Telo uplate zakupnine koje prosledjujem</param>
        /// <returns></returns>
        /// <response code="201">Kreirana je uplata</response>
        /// <response code ="500">Doslo je do greske prilikom kreiranja</response>
        /// <response code ="400">Poslat je los zahtev za kreiranje</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<UplataZakupnineConfirmationDto> CreateUplataZakupnine([FromBody] UplataZakupnineCreationDto uplataZakupnine)
        {
            try
            {
                bool modelValid = ValidateUplataZakupnine(uplataZakupnine);

                if (!modelValid)
                {
                    return BadRequest("Iznos uplate je isuvise mali");

                }

                var uplataZakupnineEntity = mapper.Map<UplataZakupnine>(uplataZakupnine); 
                var confirmation = uplataZakupnineRepository.CreateUplataZakupnine(uplataZakupnineEntity);
                uplataZakupnineRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetUplate", "UplataZakupnine", new { uplataZakupnineID = confirmation.UplataZakupnineID });
                loggerService.Log(LogLevel.Information, "PostStatus", "Uplata je uspešno kreirana!");
                return Created(location, mapper.Map<UplataZakupnineConfirmationDto>(confirmation));
            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Uplata nije kreirana, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private static bool ValidateUplataZakupnine(UplataZakupnineCreationDto uplataZakupnine)
        {
            if (uplataZakupnine.iznos < 100)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Brisanje uplate zakupnine
        /// </summary>
        /// <param name="uplataZakupnineID"></param>
        /// <returns></returns>
        /// <response code="200">Uspesno obrisana</response>
        /// <response code="404">Uplata nije pronadjena</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja uplate</response>
        /// <response code ="204" >Nakon uspesnog brisanja uplate</response>
        [HttpDelete("{UplataZakupnineID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteUplataZakupnine(Guid uplataZakupnineID)
        {
            try
            {
                var uplata = uplataZakupnineRepository.GetUplataZakupnineById(uplataZakupnineID);

                if (uplata == null)
                {

                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Uplata sa zadatim id-em nije pronađena!");
                    return NotFound();

                }
                uplataZakupnineRepository.DeleteUplataZakupnine(uplataZakupnineID);
                uplataZakupnineRepository.SaveChanges();

                loggerService.Log(LogLevel.Information, "DeleteStatus", "Uplata je uspešno obrisana!");
                return NoContent();
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Uplata nije obrisana, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }

            }
        /// <summary>
        /// Trazenje mogucih opcija
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetUplataZakupnineOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            loggerService.Log(LogLevel.Information, "GetStatus", "Opcije su uspešno vraćene!");
            return Ok();
        }

        /// <summary>
        /// Azuriranje uplate
        /// </summary>
        /// <param name="uplata"></param>
        /// <returns></returns>
        ///  <response code="201">Azurirana je uplata</response>
        /// <response code ="500">Doslo je do greske prilikom azuriranja</response>
        /// <response code = "404">Nije pronadjena uplata sa tim ID</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataZakupnineConfirmationDto> UpdateUplataZakupnine(UplataZakupnineUpdateDto uplata)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (uplataZakupnineRepository.GetUplataZakupnineById(uplata.UplataZakupnineID) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Uplata sa zadatim id-em nije pronađena!");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                UplataZakupnine uplata2 = mapper.Map<UplataZakupnine>(uplata);
                UplataZakupnineConfirmation confirmation = uplataZakupnineRepository.UpdateUplataZakupnine(uplata2);
                return Ok(mapper.Map<UplataZakupnineDto>(confirmation));

            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Uplata nije izmenjena, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
