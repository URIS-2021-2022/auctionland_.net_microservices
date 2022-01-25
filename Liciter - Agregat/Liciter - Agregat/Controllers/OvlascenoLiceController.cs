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
    class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository ovlascenoLiceRepository;
        private readonly LinkGenerator linkGenerator;

        public OvlascenoLiceController(IOvlascenoLiceRepository ovlascenoLiceRepository, LinkGenerator linkGenerator)
        {
            this.ovlascenoLiceRepository = ovlascenoLiceRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<OvlascenoLiceModel>> GetOvlascenaLicas(string JMBG_BrPasosa)
        {
            List<OvlascenoLiceModel> lica = ovlascenoLiceRepository.GetOvlascenaLicas(JMBG_BrPasosa);
            if (lica == null || lica.Count == 0)
            {
                return NoContent();
            }
            return Ok(lica);
        }

        [HttpGet("{ovlascenoLiceId}")]
        public ActionResult<OvlascenoLiceModel> GetOvlascenoLicebyId(Guid ovlascenoLiceId)
        {
            OvlascenoLiceModel ovlascenoLiceModel = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
            if (ovlascenoLiceModel == null)
            {
                return NotFound();
            }
            return Ok(ovlascenoLiceModel);
        }

        [HttpPost]
        public ActionResult<OvlascenoLiceConfirmation> CreateOvlascenoLice([FromBody] OvlascenoLiceModel ovlascenoLice)
        {
            try
            {


                OvlascenoLiceConfirmation confirmation = ovlascenoLiceRepository.CreateOvlascenoLice(ovlascenoLice);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetOvlascenoLice", "OvlascenoLice", new { ovlascenoLiceId = confirmation.OvlascenoLiceId });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpDelete("{ovlascenoLiceId}")]
        public IActionResult DeleteOvlascenoLice(Guid ovlascenoLiceId)
        {
            try
            {
                OvlascenoLiceModel ovlascenoLiceModel = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
                if (ovlascenoLiceModel == null)
                {
                    return NotFound();
                }
                ovlascenoLiceRepository.DeleteOvlascenoLice(ovlascenoLiceId);
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
