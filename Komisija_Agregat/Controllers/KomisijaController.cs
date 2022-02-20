using Komisija_Agregat.Data;
using Komisija_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Controllers
{
    [Route("api/[controller]")]
    [apiController]
    class KomisijaController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly LinkGenerator linkGenerator;

        public KomisijaController(IKomisijaRepository komisijaRepository, LinkGenerator linkGenerator)
        {
            this.komisijaRepository = komisijaRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<KomisijaModel>> GetKomisije()
        {
            List<KomisijaModel> komisije = komisijaRepository.GetKomisije();
            if (komisije == null || komisije.Count == 0)
            {
                return NoContent();
            }
            return Ok(komisije)
        }

        [HttpGet("{komisijaId}")]
        public ActionResult<KomisijaModel> GetKomisijaById(Guid komisijaId)
        {
            KomisijaModel komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
            if (komisijaModel == null)
            {
                return NotFound();
            }
            return Ok(komisijaModel)
        }

        [HttpPost]
        public ActionResult<KomisijaConfirmation> CreateKomisija([FromBody] KomisijaModel komisija)
        {
            try
            {
                KomisijaConfirmation confirmation = komisijaRepository.CreateKomisija(komisija);
                string location = linkGenerator.GetPathByAction("GetKomisija", "Komisija", new { komisijaId = confirmation.KomisijaId });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{komisijaId}")]
        public IActionResult DeleteKomisija(Guid komisijaId)
        {
            try
            {
                KomisijaModel komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
                if (komisijaModel == null)
                {
                    return NotFound();
                }
                komisijaRepository.DeleteKomisija(komisijaId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}