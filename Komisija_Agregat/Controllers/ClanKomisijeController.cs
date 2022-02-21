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
    [Route("api/[controller]")]
    [ApiController]
    class ClanKomisijeController : ControllerBase
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

        [HttpGet("{clanKomisijeId}")]
        public ActionResult<ClanKomisijeDto> GetClanKomisijeById(Guid clanKomisijeId)
        {
            var clanKomisijeModel = clanKomisijeRepository.GetClanKomisijeById(clanKomisijeId);
            if (clanKomisijeModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ClanKomisijeDto>(clanKomisijeModel));
        }

        [HttpPost]
        public ActionResult<ClanKomisijeConfirmationDto> CreateClanKomisije([FromBody] ClanKomisijeDto clanKomisije)
        {
            try
            {
                var clanKomisijeEntity = mapper.Map<ClanKomisije>(clanKomisije);
                var confirmation = clanKomisijeRepository.CreateClanKomisije(clanKomisijeEntity);
                string location = linkGenerator.GetPathByAction("GetClanKomisije", "ClanKomisije", new { clanKomisijeId = confirmation.ClanId });
                return Created(location, mapper.Map<ClanKomisijeDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{clanKomisijeId}")]
        public IActionResult DeleteClanKomisije(Guid clanKomisijeId)
        {
            try
            {
                var clanKomisijeModel = clanKomisijeRepository.GetClanKomisijeById(clanKomisijeId);
                if (clanKomisijeModel == null)
                {
                    return NotFound();
                }
                clanKomisijeRepository.DeleteClanKomisije(clanKomisijeId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

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