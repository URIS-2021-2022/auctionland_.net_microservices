using Komisija_Agregat.Data;
using Komisija_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komisija_Agregat.Entities;

namespace Komisija_Agregat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KomisijaController : ControllerBase
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

        /// <summary>
        /// Vraca sve komisije iz liste
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<KomisijaDto>> GetKomisije()
        {
            var komisije = komisijaRepository.GetKomisije();
            if (komisije == null || komisije.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KomisijaDto>>(komisije));
        }

        /// <summary>
        /// Vraca komisiju na osnovu id-a
        /// </summary>
        /// <param name="komisijaId"></param>
        /// <returns></returns>
        [HttpGet("{komisijaId}")]
        public ActionResult<KomisijaDto> GetKomisijaById(Guid komisijaId)
        {
            var komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
            if (komisijaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KomisijaDto>(komisijaModel));
        }


        /// <summary>
        /// Dodaje novu komisiju u listu
        /// </summary>
        /// <param name="komisija"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<KomisijaConfirmationDto> CreateKomisija([FromBody] KomisijaDto komisija)
        {
            try
            {
                var komisijaEntity = mapper.Map<Komisija>(komisija);
                var confirmation = komisijaRepository.CreateKomisija(komisijaEntity);
                string location = linkGenerator.GetPathByAction("GetKomisija", "Komisija", new { komisijaId = confirmation.KomisijaId });
                return Created(location, mapper.Map<KomisijaDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brise komisije sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="komisijaId"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Komisija uspesno obrisana</response>
        /// <response code="404">Nije pronadjena komisija za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja komisije</response>
        [HttpDelete("{komisijaId}")]
        public IActionResult DeleteKomisija(Guid komisijaId)
        {
            try
            {
                var komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
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

        /// <summary>
        /// Vrsi izmenu nad komisijom koji se prosledio u body-u
        /// </summary>
        /// <param name="komisija"></param>
        /// <returns>Potvrdu o modifikovanoj komisiji</returns>
        /// <response code="200">Vraca azuriranou komisiju</response>
        /// <response code="400">Komisija koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja komisije</response>
        [HttpPut]
        public ActionResult<KomisijaConfirmationDto> UpdateKomisija(KomisijaUpdateDto komisija)
        {
            try
            {
                if (komisijaRepository.GetKomisijaById(komisija.KomisijaId) == null)
                {
                    return NotFound();
                }
                Komisija komisija2 = mapper.Map<Komisija>(komisija);
                KomisijaConfirmation confirmation = komisijaRepository.UpdateKomisija(komisija2);
                return Ok(mapper.Map<KomisijaDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}