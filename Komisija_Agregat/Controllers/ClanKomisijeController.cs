using Komisija_Agregat.Data;
using Komisija_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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

        public ClanKomisijeController(IClanKomisijeRepository clanKomisijeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.clanKomisijeRepository = clanKomisijeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;   
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
                return NoContent();
            }
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
                return NotFound();
            }
            return Ok(mapper.Map<ClanKomisijeDto>(clanKomisijeModel));
        }


        /// <summary>
        /// Dodaje novog clana komisije u listu
        /// </summary>
        /// <param name="clanKomisije"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ClanKomisijeConfirmationDto> CreateClanKomisije([FromBody] ClanKomisijeDto clanKomisije)
        {
            try
            {
                var clanKomisijeEntity = mapper.Map<ClanKomisije>(clanKomisije);
                var confirmation = clanKomisijeRepository.CreateClanKomisije(clanKomisijeEntity);
                string location = linkGenerator.GetPathByAction("GetClanKomisijeById", "ClanKomisije", new { clanId = confirmation.ClanId });
                return Created(location, mapper.Map<ClanKomisijeDto>(confirmation));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error"+ex.Message);
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
                    return NotFound();
                }
                clanKomisijeRepository.DeleteClanKomisije(clanId);
                return NoContent();
            }
            catch
            {
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
                if(clanKomisijeRepository.GetClanKomisijeById(clanKomisije.ClanId) == null)
                {
                    return NotFound();
                }
                ClanKomisije clanKomisije2 = mapper.Map<ClanKomisije>(clanKomisije);
                ClanKomisijeConfirmation confirmation = clanKomisijeRepository.UpdateClanKomisije(clanKomisije2);
                return Ok(mapper.Map<ClanKomisijeDto>(confirmation));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}