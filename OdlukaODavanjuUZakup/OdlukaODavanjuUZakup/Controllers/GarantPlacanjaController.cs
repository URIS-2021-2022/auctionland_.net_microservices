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
    [Route("api/garantPlacanja")]
    public class GarantPlacanjaController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IGarantPlacanjaRepository garantPlacanjaRepository;

        public GarantPlacanjaController(IGarantPlacanjaRepository garantPlacanjaRepository, LinkGenerator linkGenerator) 
        {
            this.linkGenerator = linkGenerator;
            this.garantPlacanjaRepository = garantPlacanjaRepository;
        }

        [HttpGet]
        public ActionResult<List<GarantPlacanjaModel>> GetGaranti()
        {
            var garanti = garantPlacanjaRepository.GetGarantiPlacanja();
            if (garanti == null || garanti.Count == 0)
            {
                return NoContent();
            }
            return Ok(garanti);
            

        }
        [HttpGet("GarantiPlacanjaID")]
        public ActionResult<GarantPlacanjaModel> getGarant(Guid garantPlacanjaID)
        {
            var garant = garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanjaID);
            if (garant == null)
            {
                return NotFound();
            }
            return Ok(garant);
        }
        [HttpPost]
        public ActionResult<GarantPlacanjaModel> createGarant([FromBody] GarantPlacanjaModel garantPlacanja)
        {
            try
            {
                bool modelValid = ValidateGarantPlacanja(garantPlacanja);

                if (!modelValid)
                {
                    return BadRequest("Ne valja");
                }
                var confirmation = garantPlacanjaRepository.CreateGarantPlacanja(garantPlacanja);
                var location = linkGenerator.GetPathByAction("GetGaranti", "GarantPlacanja", new { garantPlacanjaID = confirmation.GarantPlacanjaID });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateGarantPlacanja(GarantPlacanjaModel garantPlacanja)
        {
            return true;
            // Nemam adekvatni uslov
        }
        [HttpDelete("GarantPlacanjaID")]
        public IActionResult deleteGarant(Guid garantPlacanjaID)
        {
            try
            {
                var garant = garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanjaID);
                if (garant == null)
                {
                    return NotFound();
                }
                garantPlacanjaRepository.DeleteGarantPlacanja(garantPlacanjaID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
    }
