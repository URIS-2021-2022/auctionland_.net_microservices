using AutoMapper;
using Licitacija_agregat.Data;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;

        public LicitacijaController(ILicitacijaRepository licitacijaRepository, LinkGenerator link, IMapper mapper)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.link = link;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<Licitacija>> GetLicitacijas(DateTime datum)
        {
            var licitacije = licitacijaRepository.GetLicitacijas(datum);
            if (licitacije == null || licitacije.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<Licitacija>>(licitacije));
        }

        [HttpGet("{licitacijaId}")]
        public ActionResult<LicitacijaDto> GetLicitacijaById(Guid licitacijaId)
        {
            var licitacijaModel = licitacijaRepository.GetLicitacijaById(licitacijaId);

            if(licitacijaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<LicitacijaDto>>(licitacijaModel));
        }

        [HttpPost]
        public ActionResult<LicitacijaDto> CreateLicitacija([FromBody] LicitacijaCreationDto licitacija)
        {
            try
            {
                var licitacijaEntity = mapper.Map<Licitacija>(licitacija);

                var confirmation = licitacijaRepository.CreateLicitacija(licitacijaEntity);
                string location = link.GetPathByAction("GetLicitacijas", "Licitacija", new { licitacijaId = confirmation.LicitacijaId });
                return Created(location, mapper.Map<LicitacijaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }
        }

        [HttpDelete("{LicitacijaId}")]
        public IActionResult DeleteLicitacija(Guid licitacijaId)
        {
            try
            {
                var licitacija = licitacijaRepository.GetLicitacijaById(licitacijaId);

                if(licitacija == null)
                {
                    return NotFound();
                }

                licitacijaRepository.DeleteLicitacija(licitacijaId);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        public ActionResult<LicitacijaConfirmationDto> UpdateLicitacija([FromBody] LicitacijaUpdateDto licitacija)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (licitacijaRepository.GetLicitacijaById(licitacija.LicitacijaId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Licitacija licitacijaEntity = mapper.Map<Licitacija>(licitacija);
                LicitacijaConfirmation confirmation = licitacijaRepository.UpdateLicitacija(licitacijaEntity);
                return Ok(mapper.Map<LicitacijaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }

        [HttpOptions]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
