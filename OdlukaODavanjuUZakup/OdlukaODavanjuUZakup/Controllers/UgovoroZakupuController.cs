using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OdlukaODavanjuUZakup.Data;
using OdlukaODavanjuUZakup.Entities;
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
        private readonly IMapper mapper;

        public UgovoroZakupuController(IUgovoroZakupuRepository ugovoroZakupuRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ugovoroZakupuRepository = ugovoroZakupuRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<UgovoroZakupuDto>> getUgovori(string zavodni_broj)
        {
            var ugovori = ugovoroZakupuRepository.GetUgovoriOZakupu(zavodni_broj);

            if (ugovori == null || ugovori.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<UgovoroZakupuDto>>(ugovori));
        }
        [HttpGet("UgovoroZakupuID)")]
        public ActionResult<UgovoroZakupuDto> getUgovor(Guid UgovoroZakupuID)
        {
            var ugovor = ugovoroZakupuRepository.GetUgovoriOZakupuById(UgovoroZakupuID);
            if (ugovor == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UgovoroZakupuDto>(ugovor));
        }
        [HttpPost]
        public ActionResult<UgovoroZakupuConfirmationDto> createUgovoroZakupu ([FromBody] UgovoroZakupuConfirmationDto ugovoroZakupu)
        {
            try
            {
                bool modelValid = ValidateUgovoroZakupu(ugovoroZakupu);

                if (!modelValid)
                {
                    return BadRequest("Datum za rok nije validan");
                }
                var ugovoroZakupuEntity = mapper.Map<UgovoroZakupu> (ugovoroZakupu);
                var confirmation = ugovoroZakupuRepository.CreateUgovorOZakupu(ugovoroZakupuEntity);
                var location = linkGenerator.GetPathByAction("getUgovori", "UgovoroZakupu", new { UgovoroZakupuID = confirmation.UgovoroZakupuID });
                return Created(location, confirmation);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateUgovoroZakupu(UgovoroZakupuConfirmationDto ugovoroZakupu)
        {
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
        public ActionResult<UgovoroZakupuConfirmationDto> UpdateUgovoroZakupu(UgovoroZakupuUpdateDto ugovor)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if  (ugovoroZakupuRepository.GetUgovoriOZakupuById(ugovor.UgovoroZakupuID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                UgovoroZakupu ugovor2 = mapper.Map<UgovoroZakupu>(ugovor);
                UgovoroZakupuConfirmation confirmation = ugovoroZakupuRepository.UpdateUgovorOZakupu(ugovor2);
                return Ok(mapper.Map<UgovoroZakupuDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
