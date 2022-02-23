using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.Kupac;
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
    [Route("api/kupac")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    public class KupacController : ControllerBase
    {
        private readonly IKupacRepository kupacRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IJavnaNadmetanjaService javnaNadmetanjaService;

        public KupacController(IKupacRepository kupacRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IJavnaNadmetanjaService javnaNadmetanjaService)
        {
            this.kupacRepository = kupacRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.javnaNadmetanjaService = javnaNadmetanjaService;
        }
        /// <summary>
        /// Vraća sve Kupce iz liste
        /// </summary>
        /// <param name="JMBG_MaticniBroj"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KupacDto>> GetKupci(string JMBG_MaticniBroj)
        {
            List<KupacModel> kupci = kupacRepository.GetKupci(JMBG_MaticniBroj);
            if (kupci == null || kupci.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista kupaca je prazna ili null");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista kupaca je uspesno vracena!");
            return Ok(mapper.Map<List<KupacDto>>(kupci));
        }

        /// <summary>
        /// Vraca kupca na osnovu id-a
        /// </summary>
        /// <param name="kupacId"></param>
        /// <returns></returns>
        ///  [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{kupacId}")]
        public ActionResult<KupacDto> GetKupacbyId(Guid kupacId)
        {
            KupacModel kupacModel = kupacRepository.GetKupacById(kupacId);
            if (kupacModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Kupac sa tim id-em nije pronadjeno");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Kupac sa zadatim id-em je uspesno vracena!");
            return Ok(mapper.Map<KupacDto>(kupacModel));
        }

        /// <summary>
        /// Dodaje novog kupca na listu
        /// </summary>
        /// <param name="kupac"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KupacConfirmationDto> CreateKupac([FromBody] KupacCreationDto kupac)
        {
            try
            {

                KupacModel kupac2 = mapper.Map<KupacModel>(kupac);
                KupacConfirmation confirmation = kupacRepository.CreateKupac(kupac2);
                kupacRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetKupci", "Kupac", new { kupacId = confirmation.KupacId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Kupac je uspesno napravljen!");
                return Created(location, mapper.Map<KupacConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Kupac nije kreiran, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error " + ex.InnerException.Message );
            }

        }
        /// <summary>
        /// Brise kupca sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="kupacId"></param>
        /// /// <returns>Potvrdu o izbrisanom fizickom licu</returns>
        ///  <response code="200">Vraća izbrisanog kupca</response>
        /// <response code="400">Kupac koji se brise nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja kupca</response>
        [HttpDelete("{kupacId}")]
        public IActionResult DeleteKupac(Guid kupacId)
        {
            try
            {
                KupacModel kupacModel = kupacRepository.GetKupacById(kupacId);
                if (kupacModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Kupac sa tim id-em nije pronadjen");
                    return NotFound();
                }
                kupacRepository.DeleteKupac(kupacId); 
                kupacRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Kupac je uspesno obrisan!");
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrši izmenu nad kupcem koji se prosledio u body-u
        /// </summary>
        /// <param name="kupac"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kupac uspešno ažuriran</response>
        /// <response code="404">Nije pronađen kupac za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja kupca</response>
        [HttpPut]
        public ActionResult<KupacConfirmationDto> UpdateKupac(KupacUpdateDto kupac)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (kupacRepository.GetKupacById(kupac.KupacId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Kupac sa tim id-em nije pronadjen");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                KupacModel kupacModel = mapper.Map<KupacModel>(kupac);
                KupacConfirmation confirmation = kupacRepository.UpdateKupac(kupacModel);
                kupacRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Kupac je uspesno izmenjen!");
                return Ok(mapper.Map<KupacConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene kupca");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
