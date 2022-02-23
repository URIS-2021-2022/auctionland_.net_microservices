using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.PravnoLice;
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
    [Route("api/pravnoLice")]
    [ApiController]
    [Authorize]
    public class PravnoLiceController : ControllerBase
    {
        private readonly IPravnoLiceRepository pravnoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public PravnoLiceController(IPravnoLiceRepository pravnoLiceRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.pravnoLiceRepository = pravnoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraća sva pravna lica iz liste
        /// </summary>
        /// <param name="MaticniBroj"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PravnoLiceDto>> GetPravnaLicas(string MaticniBroj)
        {
            List<PravnoLiceModel> lica = pravnoLiceRepository.GetPravnaLicas(MaticniBroj);
            if (lica == null || lica.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista pravnih lica je prazna ili null");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista pravnih lica je uspesno vracena!");
            return Ok(mapper.Map<List<PravnoLiceDto>>(lica));
        }

        /// <summary>
        /// Vraća pravno lice na osnovu id-a
        /// </summary>
        /// <param name="pravnoLiceId"></param>
        /// <returns></returns>
        ///  [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{pravnoLiceId}")]
        public ActionResult<PravnoLiceDto> GetPravnoLicebyId(Guid pravnoLiceId)
        {
            PravnoLiceModel pravnoLiceModel = pravnoLiceRepository.GetPravnoLiceById(pravnoLiceId);
            if (pravnoLiceModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Pravno lice sa tim id-em nije pronadjeno");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Pravno lice sa zadatim id-em je uspesno vracena!");
            return Ok(mapper.Map<PravnoLiceDto>(pravnoLiceModel));
        }

        /// <summary>
        /// Dodaje novo pravno lice u listu
        /// </summary>
        /// <param name="pravnoLice"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PravnoLiceConfirmationDto> CreatePravnoLice([FromBody] PravnoLiceCreationDto pravnoLice)
        {
            try
            {

                PravnoLiceModel lice = mapper.Map<PravnoLiceModel>(pravnoLice);
                PravnoLiceConfirmation confirmation = pravnoLiceRepository.CreatePravnoLice(lice);
                pravnoLiceRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetPravnoLicebyId", "PravnoLice", new { pravnoLiceId = confirmation.PravnoLiceId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Pravno lice je uspesno napravljeno!");
                return Created(location, mapper.Map<PravnoLiceConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Pravno lice nije kreirano, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error " + ex.Message);
            }

        }

        /// <summary>
        /// Briše pravno lice sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="pravnoLiceId"></param>
        /// <returns>Potvrdu o modifikovanom pravnom licu</returns>
        ///  <response code="200">Vraća ažurirano pravno lice</response>
        /// <response code="400">Pravno lice koje se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja pravnog lica</response>
        [HttpDelete("{pravnoLiceId}")]
        public IActionResult DeletePravnoLice(Guid pravnoLiceId)
        {
            try
            {
                PravnoLiceModel pravnoLiceModel = pravnoLiceRepository.GetPravnoLiceById(pravnoLiceId);
                if (pravnoLiceModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Pravno lice sa tim id-em nije pronadjeno");
                    return NotFound();
                }
                pravnoLiceRepository.DeletePravnoLice(pravnoLiceId);
                pravnoLiceRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Pravno lice je uspesno obrisano!");
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        

        /// <summary>
        /// Vrši izmenu nad pravnim licem koji se prosledio u body-u
        /// </summary>
        /// <param name="pravnoLice"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Pravno lice uspešno obrisano</response>
        /// <response code="404">Nije pronađeno pravno lice za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja pravnog lica</response>
        [HttpPut]
        public ActionResult<PravnoLiceConfirmationDto> UpdatePravnoLice(PravnoLiceUpdateDto pravnoLice)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (pravnoLiceRepository.GetPravnoLiceById(pravnoLice.PravnoLiceId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Pravno lice sa tim id-em nije pronadjeno");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                PravnoLiceModel pravnoLiceModel = mapper.Map<PravnoLiceModel>(pravnoLice);
                PravnoLiceConfirmation confirmation = pravnoLiceRepository.UpdatePravnoLice(pravnoLiceModel);
                pravnoLiceRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Pravno lice je uspesno izmenjeno!");
                return Ok(mapper.Map<PravnoLiceConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene pravnog lica");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }


        }
    }
}
