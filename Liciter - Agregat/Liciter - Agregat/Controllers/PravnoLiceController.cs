using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.PravnoLice;
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
        private readonly IMapper mapper;

        public PravnoLiceController(IPravnoLiceRepository pravnoLiceRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.pravnoLiceRepository = pravnoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<PravnoLiceDto>> GetPravnaLicas(string MaticniBroj)
        {
            List<PravnoLiceModel> lica = pravnoLiceRepository.GetPravnaLicas(MaticniBroj);
            if (lica == null || lica.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PravnoLiceDto>>(lica));
        }

        [HttpGet("{pravnoLiceId}")]
        public ActionResult<PravnoLiceDto> GetPravnoLicebyId(Guid pravnoLiceId)
        {
            PravnoLiceModel pravnoLiceModel = pravnoLiceRepository.GetPravnoLiceById(pravnoLiceId);
            if (pravnoLiceModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PravnoLiceDto>(pravnoLiceModel));
        }

        [HttpPost]
        public ActionResult<PravnoLiceConfirmationDto> CreatePravnoLice([FromBody] PravnoLiceCreationDto pravnoLice)
        {
            try
            {

                PravnoLiceModel lice = mapper.Map<PravnoLiceModel>(pravnoLice);
                PravnoLiceConfirmation confirmation = pravnoLiceRepository.CreatePravnoLice(lice);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetPravnoLice", "PravnoLice", new { pravnoLiceId = confirmation.PravnoLiceId });
                return Created(location, mapper.Map<PravnoLiceConfirmationDto>(confirmation));
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

        [HttpPut]
        public ActionResult<PravnoLiceConfirmationDto> UpdatePravnoLice(PravnoLiceUpdateDto pravnoLice)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (pravnoLiceRepository.GetPravnoLiceById(pravnoLice.PravnoLiceId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                PravnoLiceModel pravnoLiceModel = mapper.Map<PravnoLiceModel>(pravnoLice);
                PravnoLiceConfirmation confirmation = pravnoLiceRepository.UpdatePravnoLice(pravnoLiceModel);
                return Ok(mapper.Map<PravnoLiceConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
