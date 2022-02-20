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
    [Route("api/UgovorOZakupu")]
    public class UgovoroZakupuController : ControllerBase
    {
        private readonly IUgovoroZakupuRepository ugovoroZakupuRepository;
        private readonly LinkGenerator linkGenerator;

        public UgovoroZakupuController(IUgovoroZakupuRepository ugovoroZakupuRepository, LinkGenerator linkGenerator)
        {
            this.ugovoroZakupuRepository = ugovoroZakupuRepository;
            this.linkGenerator = linkGenerator;
        }
        [HttpGet]
        public ActionResult<List<UgovoroZakupuModel>> getUgovori(string zavodni_broj)
        {
            var ugovori = ugovoroZakupuRepository.GetUgovoriOZakupu(zavodni_broj);

            if (ugovori == null || ugovori.Count == 0)
            {
                return NoContent();
            }
            return Ok(ugovori);
        }
        [HttpGet("UgovoroZakupuID)")]
        public ActionResult<UgovoroZakupuModel> getUgovor(Guid UgovoroZakupuID)
        {
            var ugovor = ugovoroZakupuRepository.GetUgovoriOZakupuById(UgovoroZakupuID);
            if (ugovor == null)
            {
                return NotFound();
            }
            return Ok(ugovor);
        }
        [HttpPost]
        public ActionResult<UgovoroZakupuModel> createUgovoroZakupu ([FromBody] UgovoroZakupuModel ugovoroZakupu)
        {
            try
            {
                bool modelValid = ValidateUgovoroZakupu(ugovoroZakupu);

                if (!modelValid)
                {
                    return BadRequest("Datum za rok nije validan");
                }
                var confirmation = ugovoroZakupuRepository.CreateUgovorOZakupu(ugovoroZakupu);
                var location = linkGenerator.GetPathByAction("getUgovori", "UgovoroZakupu", new { UgovoroZakupuID = confirmation.UgovoroZakupuID });
                return Created(location, confirmation);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateUgovoroZakupu(UgovoroZakupuModel ugovoroZakupu)
        {
            if (ugovoroZakupu.rok_za_vracanje_zemljista < ugovoroZakupu.datum_zavodjenja)
            {
                return false;
            }
            return true;
        }
        [HttpDelete]
        public IActionResult deleteUgovoroZakupu (Guid UgovoroZakupuID)
        {
            {
                try
                {
                    var ugovor = ugovoroZakupuRepository.GetUgovoriOZakupuById(UgovoroZakupuID);

                    if (ugovor == null)
                    {
                        return NotFound();

                    }
                    ugovoroZakupuRepository.DeleteUgovorOZakupu(UgovoroZakupuID);
                    return NoContent();
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
                }

            }
        }
    }
}
