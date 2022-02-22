using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.OvlascenoLice;
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
   public class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository ovlascenoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OvlascenoLiceController(IOvlascenoLiceRepository ovlascenoLiceRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ovlascenoLiceRepository = ovlascenoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<OvlascenoLiceDto>> GetOvlascenaLicas(string JMBG_BrPasosa)
        {
            List<OvlascenoLiceModel> lica = ovlascenoLiceRepository.GetOvlascenaLicas(JMBG_BrPasosa);
            if (lica == null || lica.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OvlascenoLiceDto>>(lica));
        }

        [HttpGet("{ovlascenoLiceId}")]
        public ActionResult<OvlascenoLiceDto> GetOvlascenoLicebyId(Guid ovlascenoLiceId)
        {
            OvlascenoLiceModel ovlascenoLiceModel = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
            if (ovlascenoLiceModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OvlascenoLiceDto>(ovlascenoLiceModel));
        }

        [HttpPost]
        public ActionResult<OvlascenoLiceConfirmationDto> CreateOvlascenoLice([FromBody] OvlascenoLiceDto ovlascenoLice)
        {
            try
            {

                OvlascenoLiceModel lice = mapper.Map<OvlascenoLiceModel>(ovlascenoLice);
                OvlascenoLiceConfirmation confirmation = ovlascenoLiceRepository.CreateOvlascenoLice(lice);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetOvlascenoLice", "OvlascenoLice", new { ovlascenoLiceId = confirmation.OvlascenoLiceId });
                return Created(location, mapper.Map<OvlascenoLiceConfirmationDto>(confirmation));
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

        [HttpPut]
        public ActionResult<OvlascenoLiceConfirmationDto> UpdateOvlascenoLice(OvlascenoLiceUpdateDto ovlascenoLice)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLice.OvlascenoLiceId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                OvlascenoLiceModel ovlascenoLiceModel = mapper.Map<OvlascenoLiceModel>(ovlascenoLice);
                OvlascenoLiceConfirmation confirmation = ovlascenoLiceRepository.UpdateOvlascenoLice(ovlascenoLiceModel);
                return Ok(mapper.Map<OvlascenoLiceConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
