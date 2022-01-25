using Liciter___Agregat.Data;
using Liciter___Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    class KupacController : ControllerBase
    {
        private readonly IKupacRepository kupacRepository;
        private readonly LinkGenerator linkGenerator;

        public KupacController(IKupacRepository kupacRepository, LinkGenerator linkGenerator)
        {
            this.kupacRepository = kupacRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<KupacModel>> GetKupci(string JMBG_MaticniBroj)
        {
            List<KupacModel> kupci = kupacRepository.GetKupci(JMBG_MaticniBroj);
            if (kupci == null || kupci.Count == 0)
            {
                return NoContent();
            }
            return Ok(kupci);
        }

        [HttpGet("{kupacId}")]
        public ActionResult<KupacModel> GetKupacbyId(Guid kupacId)
        {
            KupacModel kupacModel = kupacRepository.GetKupacById(kupacId);
            if (kupacModel == null)
            {
                return NotFound();
            }
            return Ok(kupacModel);
        }

        [HttpPost]
        public ActionResult<KupacConfirmation> CreateKupac([FromBody] KupacModel kupac)
        {
            try
            {


                KupacConfirmation confirmation = kupacRepository.CreateKupac(kupac);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetKupac", "Kupac", new { kupacId = confirmation.KupacId });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpDelete("{kupacId}")]
        public IActionResult DeleteKupac(Guid kupacId)
        {
            try
            {
                KupacModel kupacModel = kupacRepository.GetKupacById(kupacId);
                if (kupacModel == null)
                {
                    return NotFound();
                }
                kupacRepository.DeleteKupac(kupacId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
