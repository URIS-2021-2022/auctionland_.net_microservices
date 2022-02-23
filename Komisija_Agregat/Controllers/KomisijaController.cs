using Komisija_Agregat.Data;
using Komisija_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komisija_Agregat.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Komisija_Agregat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class KomisijaController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public KomisijaController(IKomisijaRepository komisijaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.komisijaRepository = komisijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve komisije iz liste
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<KomisijaDto>> GetKomisije()
        {
            var komisije = komisijaRepository.GetKomisije();
            if (komisije == null || komisije.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista komisija je prazna ili null");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista  komisija je uspesno vracena!");
            return Ok(mapper.Map<List<KomisijaDto>>(komisije));
        }

        /// <summary>
        /// Vraca komisiju na osnovu id-a
        /// </summary>
        /// <param name="komisijaId"></param>
        /// <returns></returns>
        [HttpGet("{komisijaId}")]
        public ActionResult<KomisijaDto> GetKomisijaById(Guid komisijaId)
        {
            var komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
            if (komisijaModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Komisija sa tim id-em nije pronadjena");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Komisija sa zadatim id-em je uspesno vracena!");
            return Ok(mapper.Map<KomisijaDto>(komisijaModel));
        }


        /// <summary>
        /// Dodaje novu komisiju u listu
        /// </summary>
        /// <param name="komisija"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<KomisijaConfirmationDto> CreateKomisija([FromBody] KomisijaCreationDto komisija)
        {
            try
            {
                Komisija komisijaEntity = mapper.Map<Komisija>(komisija);
                KomisijaConfirmation confirmation = komisijaRepository.CreateKomisija(komisijaEntity);
                komisijaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKomisijaById", "Komisija", new { komisijaId = confirmation.KomisijaId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Komisija je uspesno napravljena!");
                return Created(location, mapper.Map<KomisijaConfirmationDto>(confirmation));
            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Komisija nije kreirana, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brise komisije sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="komisijaId"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Komisija uspesno obrisana</response>
        /// <response code="404">Nije pronadjena komisija za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja komisije</response>
        [HttpDelete("{komisijaId}")]
        public IActionResult DeleteKomisija(Guid komisijaId)
        {
            try
            {
                var komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
                if (komisijaModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Komisija sa tim id-em nije pronadjena");
                    return NotFound();
                }
                komisijaRepository.DeleteKomisija(komisijaId);
                komisijaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Komisija je uspesno obrisana!");
                return NoContent();
            }
            catch
            {
                loggerService.Log(LogLevel.Information, "DeleteStatus", "KOmisija nije uspesno obrisana!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrsi izmenu nad komisijom koji se prosledio u body-u
        /// </summary>
        /// <param name="komisija"></param>
        /// <returns>Potvrdu o modifikovanoj komisiji</returns>
        /// <response code="200">Vraca azuriranou komisiju</response>
        /// <response code="400">Komisija koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja komisije</response>
        [HttpPut]
        public ActionResult<KomisijaConfirmationDto> UpdateKomisija(KomisijaUpdateDto komisija)
        {
            try
            {
                if (komisijaRepository.GetKomisijaById(komisija.KomisijaId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Komisija sa tim id-em nije pronadjena");
                    return NotFound();
                }
                Komisija komisija2 = mapper.Map<Komisija>(komisija);
                KomisijaConfirmation confirmation = komisijaRepository.UpdateKomisija(komisija2);
                komisijaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Komisija je uspesno izmenjena!");
                return Ok(mapper.Map<KomisijaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene komisije");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}