using Komisija_Agregat.Data;
using Komisija_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Controllers
{
    [Route("api/[controller]")]
    [apiController]
    class ClanKomisijeController : ControllerBase
    {
        private readonly IClanKomisijeRepository clanKomisijeRepository;
        private readonly LinkGenerator linkGenerator;

        public ClanKomisijeController(IClanKomisijeRepository clanKomisijeRepository, LinkGenerator linkGenerator)
        {
            this.clanKomisijeRepository = clanKomisijeRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<ClanKomisijeModel>> GetClanovi(string ImeClana, string PrezimeClana, string EmailClana)
        {
            List<ClanKomisijeModel> clanoviKomisije = clanKomisijeRepository.GetClanovi(string ImeClana, string PrezimeClana, string EmailClana);
            if (clanoviKomisije == null || clanoviKomisije.Count == 0)
            {
                return NoContent();
            }
            return Ok(clanoviKomisije)
        }

        [HttpGet("{clanKomisijeId}")]
        public ActionResult<ClanKomisijeModel> GetClanKomisijeById(Guid clanKomisijeId)
        {
            ClanKomisijeModel clanKomisijeModel = clanKomisijeRepository.GetClanKomisijeById(clanKomisijeId);
            if (clanKomisijeModel == null)
            {
                return NotFound();
            }
            return Ok(clanKomisijeModel)
        }

        [HttpPost]
        public ActionResult<ClanKomisijeConfirmation> CreateClanKomisije([FromBody] ClanKomisijeModel clanKomisije)
        {
            try
            {
                ClanKomisijeConfirmation confirmation = clanKomisijeRepository.CreateClanKomisije(clanKomisije);
                string location = linkGenerator.GetPathByAction("GetClanKomisije", "ClanKomisije", new { clanKomisijeId = confirmation.ClanKomisijeId });
                return Created(location, confirmation);
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
                ClanKomisijeModel clanKomisijeModel = clanKomisijeRepository.GetClanKomisijeById(clanKomisijeId);
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
    }
}