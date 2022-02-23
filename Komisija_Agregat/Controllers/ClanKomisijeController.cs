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
namespace Komisija_Agregat.Controllers
{
    [Route("api/ClanKomisije")]
    [ApiController]

    public class ClanKomisijeController : ControllerBase
    {
        private readonly IClanKomisijeRepository clanKomisijeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public ClanKomisijeController(IClanKomisijeRepository clanKomisijeRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.clanKomisijeRepository = clanKomisijeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }
        /// <summary>
        /// Vraca sve clanove komisije
        /// </summary>
        /// <param name="ImeClana"></param>
        /// <param name="PrezimeClana"></param>
        /// <param name="EmailClana"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ClanKomisijeDto>> GetClanovi(string ImeClana, string PrezimeClana, string EmailClana)
        {
            var clanoviKomisije = clanKomisijeRepository.GetClanovi(ImeClana, PrezimeClana, EmailClana);
            if (clanoviKomisije == null || clanoviKomisije.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista clanova komisije je prazna ili null");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista clanova komisije je uspesno vracena!");
            return Ok(mapper.Map<List<ClanKomisijeDto>>(clanoviKomisije));
        }

        /// <summary>
        /// Vraca clana komisije na osnovu ID-a
        /// </summary>
        /// <param name="clanId"></param>
        /// <returns></returns>
        [HttpGet("{clanId}")]
        public ActionResult<ClanKomisijeDto> GetClanKomisijeById(Guid clanId)
        {
            var clanKomisijeModel = clanKomisijeRepository.GetClanKomisijeById(clanId);
            if (clanKomisijeModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Clan komisije sa tim id-em nije pronadjen");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Clan komisije sa zadatim id-em je uspesno vracen!");
            return Ok(mapper.Map<ClanKomisijeDto>(clanKomisijeModel));
        }


        /// <summary>
        /// Dodaje novog clana komisije u listu
        /// </summary>
        /// <param name="clanKomisije"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ClanKomisijeConfirmationDto> CreateClanKomisije([FromBody] ClanKomisijeCreationDto clanKomisije)
        {
            try
            {
                ClanKomisije clanKomisijeEntity = mapper.Map<ClanKomisije>(clanKomisije);
                ClanKomisijeConfirmation confirmation = clanKomisijeRepository.CreateClanKomisije(clanKomisijeEntity);
                clanKomisijeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetClanKomisijeById", "ClanKomisije", new { clanId = confirmation.ClanId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Clan komisije je uspesno napravljen!");
                return Created(location, mapper.Map<ClanKomisijeConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Clan komisije nije kreiran, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + ex.Message);
            }
        }

        /// <summary>
        /// Brise clana komisije sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="clanId"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Clan komisije uspesno obrisan</response>
        /// <response code="404">Nije pronadjen clan komisije za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja clana komisije</response>
        [HttpDelete("{clanId}")]
        public IActionResult DeleteClanKomisije(Guid clanId)
        {
            try
            {
                var clanKomisijeModel = clanKomisijeRepository.GetClanKomisijeById(clanId);
                if (clanKomisijeModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Clan komisije sa tim id-em nije pronadjeno");
                    return NotFound();
                }
                clanKomisijeRepository.DeleteClanKomisije(clanId);
                clanKomisijeRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Clan komisije je uspesno obrisan!");
                return NoContent();
            }
            catch
            {
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Clan komisije nije uspesno obrisan!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrsi izmenu nad clanom komisije koji se prosledio u body-u
        /// </summary>
        /// <param name="clanKomisije"></param>
        /// <returns>Potvrdu o modifikovanom clanu komisije</returns>
        /// <response code="200">Vraca azuriranog clana komisije</response>
        /// <response code="400">Clan komisije koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja clana komisije</response>
        [HttpPut]
        public ActionResult<ClanKomisijeConfirmationDto> UpdateClanKomisije(ClanKomisijeUpdateDto clanKomisije)
        {
            try
            {
                if (clanKomisijeRepository.GetClanKomisijeById(clanKomisije.ClanId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Clan komisije sa tim id-em nije pronadjen");
                    return NotFound();
                }
                ClanKomisije clanKomisije2 = mapper.Map<ClanKomisije>(clanKomisije);
                ClanKomisijeConfirmation confirmation = clanKomisijeRepository.UpdateClanKomisije(clanKomisije2);
                clanKomisijeRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Clan komisije je uspesno izmenjen!");
                return Ok(mapper.Map<ClanKomisijeConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene clana komisije");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}