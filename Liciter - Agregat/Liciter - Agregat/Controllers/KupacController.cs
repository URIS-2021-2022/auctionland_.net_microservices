using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.Kupac;
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
    public class KupacController : ControllerBase
    {
        private readonly IKupacRepository kupacRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KupacController(IKupacRepository kupacRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kupacRepository = kupacRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<KupacDto>> GetKupci(string JMBG_MaticniBroj)
        {
            List<KupacModel> kupci = kupacRepository.GetKupci(JMBG_MaticniBroj);
            if (kupci == null || kupci.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KupacDto>>(kupci));
        }

        [HttpGet("{kupacId}")]
        public ActionResult<KupacDto> GetKupacbyId(Guid kupacId)
        {
            KupacModel kupacModel = kupacRepository.GetKupacById(kupacId);
            if (kupacModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KupacDto>(kupacModel));
        }

        [HttpPost]
        public ActionResult<KupacConfirmationDto> CreateKupac([FromBody] KupacCreationDto kupac)
        {
            try
            {

                KupacModel kupac2 = mapper.Map<KupacModel>(kupac);
                KupacConfirmation confirmation = kupacRepository.CreateKupac(kupac2);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetKupac", "Kupac", new { kupacId = confirmation.KupacId });
                return Created(location, mapper.Map<KupacConfirmationDto>(confirmation));
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

        [HttpPut]
        public ActionResult<KupacConfirmationDto> UpdateKupac(KupacUpdateDto kupac)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (kupacRepository.GetKupacById(kupac.KupacId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                KupacModel kupacModel = mapper.Map<KupacModel>(kupac);
                KupacConfirmation confirmation = kupacRepository.UpdateKupac(kupacModel);
                return Ok(mapper.Map<KupacConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
