using Licitacija_agregat.Data;
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

        public EtapaController(IEtapaRepository etapaRepository, LinkGenerator link)
        {
            this.etapaRepository = etapaRepository;
            this.link = link;
        }

        [HttpGet]
        public ActionResult<List<EtapaModel>> GetEtapas(DateTime dan)
        {
            var etape = etapaRepository.GetEtapas(dan);
            if (etape == null || etape.Count == 0)
            {
                return NoContent();
            }
            return Ok(etape);
        }

        [HttpGet("{etapaId}")]
        public ActionResult<EtapaModel> GetEtapaById(Guid etapaId)
        {
            EtapaModel etapaModel = etapaRepository.GetEtapaById(etapaId);

            if(etapaModel == null)
            {
                return NotFound();
            }
            return Ok(etapaModel);
        }

        [HttpPost]
        public ActionResult<EtapaModel> CreateEtapa([FromBody] EtapaModel etapa)
        {
            try
            {
                EtapaConfirmation confirmation = etapaRepository.CreateEtapa(etapa);
                string location = link.GetPathByAction("GetEtapas", "Etapa", new { etapaId = confirmation.EtapaId });
                return Created(location, confirmation);
                    
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }
        }

        [HttpDelete]
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
    }
}
