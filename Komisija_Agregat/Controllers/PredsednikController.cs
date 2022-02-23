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

namespace Komisija_Agregat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PredsednikController : ControllerBase
    {
        private readonly IPredsednikRepository predsednikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public PredsednikController(IPredsednikRepository predsednikRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.predsednikRepository = predsednikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve predsednike komisija
        /// </summary>
        /// <param name="ImePredsednika"></param>
        /// <param name="PrezimePredsednika"></param>
        /// <param name="EmailPredsednika"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<PredsednikDto>> GetPredsednici(string ImePredsednika, string PrezimePredsednika, string EmailPredsednika)
        {
            var predsednici = predsednikRepository.GetPredsednici(ImePredsednika, PrezimePredsednika, EmailPredsednika);
            if (predsednici == null || predsednici.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista predsednika komisije je prazna ili null");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista predsednika komisije je uspesno vracena!");
            return Ok(mapper.Map<List<PredsednikDto>>(predsednici));
        }

        /// <summary>
        /// Vraca predsednika na osnovu ID
        /// </summary>
        /// <param name="predsednikId"></param>
        /// <returns></returns>
        [HttpGet("{predsednikId}")]
        public ActionResult<PredsednikDto> GetPredsednikById(Guid predsednikId)
        {
            var predsednik = predsednikRepository.GetPredsednikById(predsednikId);
            if (predsednik == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Predsednik komisije sa tim id-em nije pronadjen");
                return NotFound();

            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Predsednik komisije sa zadatim id-em je uspesno vracen!");
            return Ok(mapper.Map<PredsednikDto>(predsednik));
        }

        /// <summary>
        /// Dodaje novog predsednika komisije u listu
        /// </summary>
        /// <param name="predsednik"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<PredsednikConfirmationDto> CreatePredsednik([FromBody] PredsednikCreationDto predsednik)
        {
            try
            {
                Predsednik predsednikEntity = mapper.Map<Predsednik>(predsednik);
                PredsednikConfirmation confirmation = predsednikRepository.CreatePredsednik(predsednikEntity);
                predsednikRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPredsednikById", "Predsednik", new { predsednikId = confirmation.PredsednikId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Predsednik komisije je uspesno napravljen!");
                return Created(location, mapper.Map<PredsednikConfirmationDto>(confirmation));
            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Predsednik komisije nije kreiran, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Brise predsednika komisije sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="predsednikId"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Predsednik uspesno obrisan</response>
        /// <response code="404">Nije pronadjen predsednik za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja predsednika</response>
        [HttpDelete("{predsednikId}")]
        public IActionResult DeletePredsednik(Guid predsednikId)
        {
            try
            {
                var predsednik = predsednikRepository.GetPredsednikById(predsednikId);
                if (predsednik == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Predsednik komisije sa tim id-em nije pronadjen");
                    return NotFound();
                }
                predsednikRepository.DeletePredsednik(predsednikId);
                predsednikRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Predsednik komisije je uspesno obrisan!");
                return NoContent();
            }
            catch
            {
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Predsednik komisije nije uspesno obrisan!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrsi izmenu nad predsednikom komisije koji se prosledio u body-u
        /// </summary>
        /// <param name="predsednik"></param>
        /// <returns>Potvrdu o modifikovanom predsedniku komisije</returns>
        /// <response code="200">Vraca azuriranog predsednika komisije</response>
        /// <response code="400">Predsednik komisije koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja predsednika komisije</response>
        [HttpPut]
        public ActionResult<PredsednikConfirmationDto> UpdatePredsednik(PredsednikUpdateDto predsednik)
        {
            try
            {
                if (predsednikRepository.GetPredsednikById(predsednik.PredsednikId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Predsednik komisije sa tim id-em nije pronadjen");
                    return NotFound();
                }
                Predsednik predsednik2 = mapper.Map<Predsednik>(predsednik);
                PredsednikConfirmation confirmation = predsednikRepository.UpdatePredsednik(predsednik2);
                predsednikRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Predsednik komisije je uspesno izmenjen!");
                return Ok(mapper.Map<PredsednikConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene predsednika komisije");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}