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
    class FizickoLiceController : ControllerBase
    {
        private readonly IFizickoLiceRepository fizickoLiceRepository;
        private readonly LinkGenerator linkGenerator;

        public FizickoLiceController(IFizickoLiceRepository fizickoLiceRepository, LinkGenerator linkGenerator)
        {
            this.fizickoLiceRepository = fizickoLiceRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<FizickoLiceModel>> GetFizickaLicas(string JMBG)
        {
            List<FizickoLiceModel> lica = fizickoLiceRepository.GetFizickaLicas(JMBG);
            if (lica == null || lica.Count == 0)
            {
                return NoContent();
            }
            return Ok(lica);
        }

        [HttpGet("{fizickoLiceId}")]
        public ActionResult<FizickoLiceModel> GetFizickoLicebyId(Guid fizickoLiceId)
        {
            FizickoLiceModel fizickoLiceModel = fizickoLiceRepository.GetFizickoLiceById(fizickoLiceId);
            if (fizickoLiceModel == null)
            {
                return NotFound();
            }
            return Ok(fizickoLiceModel);
        }

        [HttpPost]
        public ActionResult<PravnoLiceConfirmation> CreateFizickoLice([FromBody] FizickoLiceModel fizickoLice)
        {
            try
            {


                FizickoLiceConfirmation confirmation = fizickoLiceRepository.CreateFizickoLice(fizickoLice);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetFizickoLice", "FizickoLice", new { fizickoLiceId = confirmation.FizickoLiceId });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpDelete("{fizickoLiceId}")]
        public IActionResult DeleteFizickoLice(Guid fizickoLiceId)
        {
            try
            {
                FizickoLiceModel fizickoLiceModel = fizickoLiceRepository.GetFizickoLiceById(fizickoLiceId);
                if (fizickoLiceModel == null)
                {
                    return NotFound();
                }
                fizickoLiceRepository.DeleteFizickoLice(fizickoLiceId);
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
