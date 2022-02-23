using Program_Agregat.Data;
using Program_Agregat.Entities;
using Program_Agregat.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Program_Agregat.ServiceCalls;

namespace Program_Agregat.Controllers
{
    [ApiController]
    [Route("api/PredlogPlana")]
    [Authorize]
    public class PredlogPlanaController : ControllerBase
    {
        private readonly IPredlogPlanaRepository predlogPlanaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IKomisijaService KomisijaService;

        public PredlogPlanaController(IPredlogPlanaRepository predlogPlanaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKomisijaService komisijaService)
        {
            this.predlogPlanaRepository = predlogPlanaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.KomisijaService = komisijaService;
        }
        /// <summary>
        /// Vraca sve predloge plana
        /// </summary>
        /// <param name="BrojDokumenta"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<PredlogPlanaDto>> GetPredloziPlana(string BrojDokumenta)
        {
            List<PredlogPlana> predloziPlana = predlogPlanaRepository.GetPredloziPlana(BrojDokumenta);
            if (predloziPlana == null || predloziPlana.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista predloga planova je prazna ili null");
                return NoContent();
            }
            List<PredlogPlanaDto> predlogPlanaDtoList = mapper.Map<List<PredlogPlanaDto>>(predloziPlana);

            foreach (PredlogPlanaDto odto in predlogPlanaDtoList)
            {

                odto.Komisija = KomisijaService.GetKomisijaByIdAsync(odto.KomisijaId, Request).Result;
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista predloga planova je uspesno vracena!");
            return Ok(mapper.Map<List<PredlogPlanaDto>>(predloziPlana));
        }

        /// <summary>
        /// Vraca predlog plana na osnovu ID-a
        /// </summary>
        /// <param name="predlogPlanaId"></param>
        /// <returns></returns>
        [HttpGet("{predlogPlanaId}")]
        public ActionResult<PredlogPlanaDto> GetPredlogPlanaById(Guid predlogPlanaId)
        {
            var predlogPlanaModel = predlogPlanaRepository.GetPredlogPlanaById(predlogPlanaId);
            if (predlogPlanaModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Predlog plana sa tim id-em nije pronadjen");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Predlog plana sa zadatim id-em je uspesno vracen!");
            return Ok(mapper.Map<PredlogPlanaDto>(predlogPlanaModel));
        }

        /// <summary>
        /// Dodaje novi predlog plana u listu
        /// </summary>
        /// <param name="predlogPlana"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PredlogPlanaConfirmationDto> CreatePredlogPlana([FromBody] PredlogPlanaCreationDto predlogPlana)
        {
            try
            {
                PredlogPlana predlogPlanaEntity = mapper.Map<PredlogPlana>(predlogPlana);
                PredlogPlanaConfirmation confirmation = predlogPlanaRepository.CreatePredlogPlana(predlogPlanaEntity);
                predlogPlanaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPredlogPlanaById", "PredlogPlana", new { predlogPlanaId = confirmation.PredlogPlanaId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Predlog plana je uspesno napravljen!");
                return Created(location, mapper.Map<PredlogPlanaConfirmationDto>(confirmation));
            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Predlog plana nije kreiran, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brise predlog plana sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="predlogPlanaId"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Predlog plana uspesno obrisan</response>
        /// <response code="404">Nije pronadjen predlog plana za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja predloga plana</response>
        [HttpDelete("{predlogPlanaId}")]
        public IActionResult DeletePredlogPlana(Guid predlogPlanaId)
        {
            try
            {
                var predlogPlanaModel = predlogPlanaRepository.GetPredlogPlanaById(predlogPlanaId);
                if (predlogPlanaModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Predlog plana sa tim id-em nije pronadjen");
                    return NotFound();
                }
                predlogPlanaRepository.DeletePredlogPlana(predlogPlanaId);
                predlogPlanaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Predlog plana je uspesno obrisan!");
                return NoContent();
            }
            catch
            {
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Predlog plana nije uspesno obrisan!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrsi izmenu nad predlogom plana koji je prosledjen u body-u
        /// </summary>
        /// <param name="predlogPlana"></param>
        /// <returns>Potvrda o modifikovanom predlogu plana</returns>
        /// <response code="200">Vraca azurirani predlog plana</response>
        /// <response code="400">Predlog plana koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja predloga plana</response>
        [HttpPut]
        public ActionResult<PredlogPlanaConfirmationDto> UpdatePredlogPlana(PredlogPlanaUpdateDto predlogPlana)
        {
            try
            {
                if (predlogPlanaRepository.GetPredlogPlanaById(predlogPlana.PredlogPlanaId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Predlog plana sa tim id-em nije pronadjen");
                    return NotFound();
                }
                PredlogPlana predlogPlana2 = mapper.Map<PredlogPlana>(predlogPlana);
                PredlogPlanaConfirmation confirmation = predlogPlanaRepository.UpdatePredlogPlana(predlogPlana2);
                predlogPlanaRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Predlog plana je uspesno izmenjen!");
                return Ok(mapper.Map<PredlogPlanaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene predloga plana");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}