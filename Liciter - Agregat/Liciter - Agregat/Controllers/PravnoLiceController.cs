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
    class PravnoLiceController : ControllerBase
    {
        private readonly IPravnoLiceRepository pravnoLiceRepository;
        private readonly LinkGenerator linkGenerator;

        public PravnoLiceController(IPravnoLiceRepository pravnoLiceRepository, LinkGenerator linkGenerator)
        {
            this.pravnoLiceRepository = pravnoLiceRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<PravnoLiceModel>> GetPravnaLicas(string MaticniBroj)
        {
            List<PravnoLiceModel> lica = pravnoLiceRepository.GetPravnaLicas(MaticniBroj);
            if (lica == null || lica.Count == 0)
            {
                return NoContent();
            }
            return Ok(lica);
        }

        [HttpGet("{pravnoLiceId}")]
        public ActionResult<PravnoLiceModel> GetPravnoLicebyId(Guid pravnoLiceId)
        {
            PravnoLiceModel pravnoLiceModel = pravnoLiceRepository.GetPravnoLiceById(pravnoLiceId);
            if (pravnoLiceModel == null)
            {
                return NotFound();
            }
            return Ok(pravnoLiceModel);
        }

        [HttpPost]
        public ActionResult<PravnoLiceConfirmation> CreatePravnoLice([FromBody] PravnoLiceModel pravnoLice)
        {
            try
            {
                

                PravnoLiceConfirmation confirmation = pravnoLiceRepository.CreatePravnoLice(pravnoLice);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetPravnoLice", "PravnoLice", new { pravnoLiceId = confirmation.PravnoLiceId });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpDelete("{pravnoLiceId}")]
        public IActionResult DeletePravnoLice(Guid pravnoLiceId)
        {
            try
            {
                PravnoLiceModel pravnoLiceModel = pravnoLiceRepository.GetPravnoLiceById(pravnoLiceId);
                if (pravnoLiceModel == null)
                {
                    return NotFound();
                }
                pravnoLiceRepository.DeletePravnoLice(pravnoLiceId);
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
