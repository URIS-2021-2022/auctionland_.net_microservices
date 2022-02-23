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
    /// Controller za entitet odluka o davanju u zakup
    /// </summary>
    [ApiController]
    [Route("api/Odluke")]
    [Produces("application/json", "application/xml")]
    public class OdlukaoDavanjuuZakupController : ControllerBase
    {

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository;
        private readonly ILoggerService loggerService;

        /// <summary>
        /// 
        /// Konstruktor
        /// </summary>
        /// <param name="odlukaoDavanjuuZakupRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        public OdlukaoDavanjuuZakupController(IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)

        {
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.odlukaoDavanjuuZakupRepository = odlukaoDavanjuuZakupRepository;
            this.loggerService = loggerService;
        }
        /// <summary>
        /// Vraca listu svih odluka o davanju u zakup
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Uspesno vraca sve odluke</response>
        /// <response code="404">Nema odluka u bazi</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpHead]
        public ActionResult<List<OdlukaoDavanjuuZakupDto>> GetOdluke()
        {
            var odluke = odlukaoDavanjuuZakupRepository.GetOdluke();
            if (odluke == null || odluke.Count == 0)
            {

                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista odluka je prazna ili null.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista odluka je uspešno vraćena!");
            return Ok(mapper.Map<List<OdlukaoDavanjuuZakupDto>>(odluke));
        }
        /// <summary>
        /// Vraca Odluku sa prosledjenim ID
        /// </summary>
        /// <param name="odlukaoDavanjuuZakupID">ID odluke koju trazimo</param>
        /// <returns></returns>
        /// <response code="200">Uspesno vraca pronadjenu odluku</response>
        /// <response code="404">Ne postoji takva odluka u bazi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{OdlukaoDavanjuuZakupID}")]
        public ActionResult<OdlukaoDavanjuuZakupDto> GetOdluka (Guid odlukaoDavanjuuZakupID)
        {
            var odluka = odlukaoDavanjuuZakupRepository.GetOdlukaById(odlukaoDavanjuuZakupID);
            if (odluka == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Odluka sa tim id-em nije pronađena.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Odluka sa zadatim id-em je uspešno vraćena!");
            return Ok(mapper.Map<OdlukaoDavanjuuZakupDto>(odluka));
         }
        /// <summary>
        /// Kreira se nova odluka o davanju u zakup u bazi
        /// </summary>
        /// <param name="odlukaoDavanjuuZakup">Prosledjen body odluke</param>
        /// <returns></returns>
        /// <response code="201">Uspesno je kreirana</response>
        /// <response code="500">Doslo je do greske prilikom kreiranja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OdlukaoDavanjuuZakupDto> CreateOdluka([FromBody] OdlukaoDavanjuuZakupCreationDto odlukaoDavanjuuZakup)
        {
            try
            {
                var odlukaoDavanjuuZakupEntity = mapper.Map<OdlukaoDavanjuuZakup>(odlukaoDavanjuuZakup);
                var confirmation = odlukaoDavanjuuZakupRepository.CreateOdluka(odlukaoDavanjuuZakupEntity);
                odlukaoDavanjuuZakupRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetOdluke", "OdlukaoDavanjuuZakup", new { odlukaoDavanjuuZakupID = confirmation.OdlukaoDavanjuuZakupID });
                loggerService.Log(LogLevel.Information, "PostStatus", "Odluka je uspešno kreirana!");
                return Created(location, mapper.Map<OdlukaoDavanjuuZakupConfirmationDto>(confirmation));

            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Odluka nije kreirana, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        /// <summary>
        /// Brisanje odluke sa odredjenim ID iz baze
        /// </summary>
        /// <param name="odlukaoDavanjuuZakupID"></param>
        /// <returns></returns>
        /// <response code="200">Vraca izbrisanu odluku</response>
        /// <response code="404">Odluka nije pronadjena</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja odluke</response>
        /// <response code ="204" >Nakon uspesnog brisanja odluke</response>
        [HttpDelete("{OdlukaoDavanjuuZakupID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteOdluka(Guid odlukaoDavanjuuZakupID)
        {
            try
            {
                var odluka = odlukaoDavanjuuZakupRepository.GetOdlukaById(odlukaoDavanjuuZakupID);
                if (odluka == null)
                {

                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Odluka sa zadatim id-em nije pronađena!");
                    return NotFound();
                }
                odlukaoDavanjuuZakupRepository.DeleteOdluka(odlukaoDavanjuuZakupID);
                odlukaoDavanjuuZakupRepository.SaveChanges();

                loggerService.Log(LogLevel.Information, "DeleteStatus", "Odluka je uspešno obrisana!");
                return NoContent();
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Odluka nije obrisana, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        /// <summary>
        /// Azuriranje 
        /// </summary>
        /// <param name="odluka"></param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OdlukaoDavanjuuZakupConfirmationDto> UpdateOdlukaoDavanjuuZakup(OdlukaoDavanjuuZakupUpdateDto odluka)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (odlukaoDavanjuuZakupRepository.GetOdlukaById(odluka.OdlukaoDavanjuuZakupID) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Odluka sa zadatim id-em nije pronađena!");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                OdlukaoDavanjuuZakup odluka2 = mapper.Map<OdlukaoDavanjuuZakup>(odluka);
                OdlukaoDavanjuuZakupConfirmation confirmation = odlukaoDavanjuuZakupRepository.UpdateOdluka(odluka2);
                return Ok(mapper.Map<OdlukaoDavanjuuZakupDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Odluka nije izmenjena, došlo je do greške!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        [HttpOptions]
        public IActionResult GetOdlukaODavanjuUZakupOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            loggerService.Log(LogLevel.Information, "GetStatus", "Opcije su uspešno vraćene!");
            return Ok();
        }
    }
}
