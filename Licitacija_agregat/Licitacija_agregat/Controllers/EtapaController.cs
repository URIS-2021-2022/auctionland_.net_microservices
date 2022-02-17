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
    public class EtapaController : ControllerBase
    {
        private readonly IEtapaRepository etapaRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;

        public EtapaController(IEtapaRepository etapaRepository, LinkGenerator link, IMapper mapper)
        {
            this.etapaRepository = etapaRepository;
            this.link = link;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<Etapa>> GetEtapas(DateTime dan)
        {
            var etape = etapaRepository.GetEtapas(dan);

            if (etape == null || etape.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<Etapa>>(etape));
        }

        [HttpGet("{etapaId}")]
        public ActionResult<EtapaDto> GetEtapaById(Guid etapaId)
        {
            var etapaModel = etapaRepository.GetEtapaById(etapaId);

            if(etapaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<EtapaDto>(etapaModel));
        }

        [HttpPost]
        public ActionResult<EtapaDto> CreateEtapa([FromBody] EtapaCreationDto etapa)
        {
            try
            {
                var etapaEntity = mapper.Map<Etapa>(etapa);

                var confirmation = etapaRepository.CreateEtapa(etapaEntity);

                string location = link.GetPathByAction("GetEtapas", "Etapa", new { etapaId = confirmation.EtapaId });
                return Created(location, mapper.Map<EtapaConfirmationDto>(confirmation));
                    
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }
        }

        [HttpDelete("{EtapaId}")]
        public IActionResult DeleteEtapa(Guid etapaId)
        {
            try
            {
                var etapa = etapaRepository.GetEtapaById(etapaId);

                if(etapa == null)
                {
                    return NotFound();
                }

                etapaRepository.DeleteEtapa(etapaId);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        public ActionResult<EtapaConfirmationDto> UpdateEtapa(EtapaUpdateDto etapa)
        {
            try
            {
                if(etapaRepository.GetEtapaById(etapa.EtapaId) == null)
                {
                    return NotFound();
                }

                Etapa etapaEntity = mapper.Map<Etapa>(etapa);
                var confirmation = etapaRepository.UpdateEtapa(etapaEntity);
                return Ok(mapper.Map<EtapaConfirmationDto>(confirmation));


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }

        [HttpOptions]
        public IActionResult GetEtapaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
