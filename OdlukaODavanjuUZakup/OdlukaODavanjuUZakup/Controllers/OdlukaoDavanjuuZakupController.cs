using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OdlukaODavanjuUZakup.Data;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Controllers
{
    [ApiController]
    [Route("api/Odluke")]
    public class OdlukaoDavanjuuZakupController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository;

        public OdlukaoDavanjuuZakupController(IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository, LinkGenerator linkGenerator)

        {
            this.linkGenerator = linkGenerator;
            this.odlukaoDavanjuuZakupRepository = odlukaoDavanjuuZakupRepository;
        }
        [HttpGet]
        public ActionResult<List<OdlukaoDavanjuuZakupModel>> GetOdluke()
        {
            var odluke = odlukaoDavanjuuZakupRepository.GetOdluke();
            if (odluke == null || odluke.Count == 0)
            {
                return NoContent();
            }
            return Ok(odluke);
        }
        [HttpGet("OdlukaoDavanjuuZakupID")]
        public ActionResult<OdlukaoDavanjuuZakupModel> getOdluka (Guid odlukaoDavanjuuZakupID)
        {
            var odluka = odlukaoDavanjuuZakupRepository.GetOdlukaById(odlukaoDavanjuuZakupID);
            if (odluka == null)
            {
                return NotFound();
            }
            return Ok(odluka);
         }
        [HttpPost]
        public ActionResult<OdlukaoDavanjuuZakupModel> createOdluka([FromBody] OdlukaoDavanjuuZakupModel odlukaoDavanjuuZakup)
        {
            try
            {
                bool modelValid = ValidateOdlukaoDavanjuuZakup(odlukaoDavanjuuZakup);

                if (!modelValid)
                {
                    return BadRequest("Ne valja");
                }
                var confirmation = odlukaoDavanjuuZakupRepository.CreateOdluka(odlukaoDavanjuuZakup);
                var location = linkGenerator.GetPathByAction("GetOdluke", "OdlukaoDavanjuuZakup", new { odlukaoDavanjuuZakupID = confirmation.OdlukaoDavanjuuZakupID });
                return Created(location, confirmation);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateOdlukaoDavanjuuZakup(OdlukaoDavanjuuZakupModel odlukaoDavanjuuZakup)
        {
            return true;
            // Nemam adekvatni uslov
        }
        [HttpDelete]
        public IActionResult deleteOdluka(Guid odlukaoDavanjuuZakupID)
        {
            try
            {
                var odluka = odlukaoDavanjuuZakupRepository.GetOdlukaById(odlukaoDavanjuuZakupID);
                if (odluka == null)
                {
                    return NotFound();
                }
                odlukaoDavanjuuZakupRepository.DeleteOdluka(odlukaoDavanjuuZakupID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
}
