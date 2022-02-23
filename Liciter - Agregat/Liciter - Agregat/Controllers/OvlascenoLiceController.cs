using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.OvlascenoLice;
using Liciter___Agregat.Models;
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

namespace Liciter___Agregat.Controllers
{
    [Route("api/ovlascenoLice")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    public class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository ovlascenoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public OvlascenoLiceController(IOvlascenoLiceRepository ovlascenoLiceRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.ovlascenoLiceRepository = ovlascenoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraća sva ovlascena lica iz liste
        /// </summary>
        /// <param name="JMBG_BrPasosa"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OvlascenoLiceDto>> GetOvlascenaLicas(string JMBG_BrPasosa)
        {
            List<OvlascenoLiceModel> lica = ovlascenoLiceRepository.GetOvlascenaLicas(JMBG_BrPasosa);
            if (lica == null || lica.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista ovlascenih lica je prazna ili null");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista ovlascenih lica je uspesno vracena!");
            return Ok(mapper.Map<List<OvlascenoLiceDto>>(lica));
        }

        /// <summary>
        /// Vraća ovlasceno lice na osnovu id-a
        /// </summary>
        /// <param name="ovlascenoLiceId"></param>
        /// <returns></returns>
        ///  [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{ovlascenoLiceId}")]
        public ActionResult<OvlascenoLiceDto> GetOvlascenoLicebyId(Guid ovlascenoLiceId)
        {
            OvlascenoLiceModel ovlascenoLiceModel = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
            if (ovlascenoLiceModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Ovlasceno lice sa tim id-em nije pronadjeno");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Ovlasceno lice sa zadatim id-em je uspesno vracena!");
            return Ok(mapper.Map<OvlascenoLiceDto>(ovlascenoLiceModel));
        }

        /// <summary>
        /// Dodaje novo ovlasceno lice u listu
        /// </summary>
        /// <param name="ovlascenoLice"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OvlascenoLiceConfirmationDto> CreateOvlascenoLice([FromBody] OvlascenoLiceCreationDto ovlascenoLice)
        {
            try
            {

                OvlascenoLiceModel lice = mapper.Map<OvlascenoLiceModel>(ovlascenoLice);
                OvlascenoLiceConfirmation confirmation = ovlascenoLiceRepository.CreateOvlascenoLice(lice);
                ovlascenoLiceRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetOvlascenoLicebyId", "OvlascenoLice", new { ovlascenoLiceId = confirmation.OvlascenoLiceId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Ovlasceno lice je uspesno napravljeno!");
                return Created(location, mapper.Map<OvlascenoLiceConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Ovlasceno lice nije kreirano, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error " + ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Briše ovlasceno lice sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="ovlascenoLiceId"></param>
        /// <returns>Potvrdu o modifikovanom ovlascenom licu</returns>
        ///  <response code="200">Vraća ažurirano ovlasceno lice</response>
        /// <response code="400">Ovlasceno lice koje se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ovlascenog lica</response>
        [HttpDelete("{ovlascenoLiceId}")]
        public IActionResult DeleteOvlascenoLice(Guid ovlascenoLiceId)
        {
            try
            {
                OvlascenoLiceModel ovlascenoLiceModel = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
                if (ovlascenoLiceModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Ovlasceno lice sa tim id-em nije pronadjeno");
                    return NotFound();
                }
                ovlascenoLiceRepository.DeleteOvlascenoLice(ovlascenoLiceId);
                ovlascenoLiceRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Ovlasceno lice je uspesno obrisano!");
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrši izmenu nad ovlascenim licem koji se prosledio u body-u
        /// </summary>
        /// <param name="ovlascenoLice"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ovlasceno lice uspešno obrisano</response>
        /// <response code="404">Nije pronađeno ovlasceno lice za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ovlascenog lica</response>
        [HttpPut]
        public ActionResult<OvlascenoLiceConfirmationDto> UpdateOvlascenoLice(OvlascenoLiceUpdateDto ovlascenoLice)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLice.OvlascenoLiceId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Ovlasceno lice sa tim id-em nije pronadjeno");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                OvlascenoLiceModel ovlascenoLiceModel = mapper.Map<OvlascenoLiceModel>(ovlascenoLice);
                OvlascenoLiceConfirmation confirmation = ovlascenoLiceRepository.UpdateOvlascenoLice(ovlascenoLiceModel);
                ovlascenoLiceRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Ovlasceno lice je uspesno izmenjeno!");
                return Ok(mapper.Map<OvlascenoLiceConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene ovlascenog lica");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }


        }
    }
}
