using Komisija_Agregat.Data;
using Komisija_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.linq;
using System.Threading.Tasks;
using AutoMapper;
using Komisija_Agregat.Entities;

namespace Komisija_Agregat.Controllers
{
    [Route("api/[controller]")]
    [apiController]
    class KomisijaController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KomisijaController(IKomisijaRepository komisijaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.komisijaRepository = komisijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;   
        }

        [HttpGet]
        public ActionResult<List<KomisijaDto>> GetKomisije()
        {
            List<KomisijaDto> komisije = komisijaRepository.GetKomisije();
            if (komisije == null || komisije.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KomisijaDto>>(komisije));
        }

        [HttpGet("{komisijaId}")]
        public ActionResult<KomisijaDto> GetKomisijaById(Guid komisijaId)
        {
            KomisijaDto komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
            if (komisijaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KomisijaDto>(komisijaModel));
        }

        [HttpPost]
        public ActionResult<KomisijaConfirmationDto> CreateKomisija([FromBody] KomisijaDto komisija)
        {
            try
            {
                var komisijaEntity = mapper.Map<Komisija>(komisija);
                KomisijaConfirmationDto confirmation = komisijaRepository.CreateKomisija(komisijaEntity);
                string location = linkGenerator.GetPathByAction("GetKomisija", "Komisija", new { komisijaId = confirmation.KomisijaId });
                return Created(location, mapper.Map<KomisijaDto>(confirmation));
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
                KomisijaDto komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
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