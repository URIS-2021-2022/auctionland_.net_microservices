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
    public class PredsednikController : ControllerBase
    {
        private readonly IPredsednikRepository predsednikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public PredsednikController(IPredsednikRepository predsednikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.predsednikRepository = predsednikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve predsednike komisija
        /// </summary>
        /// <param name="ImePredsednika"></param>
        /// <param name="PrezimePredsednika"></param>
        /// <param name="EmailPredsednika"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<PredsednikDto>> GetPredsednici(string ImePredsednika, string PrezimePredsednika, string EmailPredsednika)
        {
            var predsednici = predsednikRepository.GetPredsednici(ImePredsednika,PrezimePredsednika,EmailPredsednika);
            if (predsednici == null || predsednici.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PredsednikDto>>(predsednici));
        }

        /// <summary>
        /// Vraca predsednika na osnovu ID
        /// </summary>
        /// <param name="predsednikId"></param>
        /// <returns></returns>
        [HttpGet("{predsednikId}")]
        public ActionResult<PredsednikDto> GetPredsednikById(Guid predsednikId)
        {
            var predsednik = predsednikRepository.GetPredsednikById(predsednikId);
            if (predsednik == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PredsednikDto>(predsednik));
        }

        /// <summary>
        /// Dodaje novog predsednika komisije u listu
        /// </summary>
        /// <param name="predsednik"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<PredsednikConfirmationDto> CreatePredsednik([FromBody] PredsednikDto predsednik)
        {
            try
            {
                var predsednikEntity = mapper.Map<Predsednik>(predsednik);
                var confirmation = predsednikRepository.CreatePredsednik(predsednikEntity);
                string location = linkGenerator.GetPathByAction("GetPredsednik", "Predsednik", new { predsednikId = confirmation.PredsednikId });
                return Created(location, mapper.Map<PredsednikDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Brise predsednika komisije sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="predsednikId"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Predsednik uspesno obrisan</response>
        /// <response code="404">Nije pronadjen predsednik za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja predsednika</response>
        [HttpDelete("{predsednikId}")]
        public IActionResult DeletePredsednik(Guid predsednikId)
        {
            try
            {
                var predsednik = predsednikRepository.GetPredsednikById(predsednikId);
                if (predsednik == null)
                {
                    return NotFound();
                }
                predsednikRepository.DeletePredsednik(predsednikId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrsi izmenu nad predsednikom komisije koji se prosledio u body-u
        /// </summary>
        /// <param name="predsednik"></param>
        /// <returns>Potvrdu o modifikovanom predsedniku komisije</returns>
        /// <response code="200">Vraca azuriranog predsednika komisije</response>
        /// <response code="400">Predsednik komisije koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja predsednika komisije</response>
        [HttpPut]
        public ActionResult<PredsednikConfirmationDto> UpdatePredsednik(PredsednikUpdateDto predsednik)
        {
            try
            {
                if (predsednikRepository.GetPredsednikById(predsednik.PredsednikId) == null)
                {
                    return NotFound();
                }
                Predsednik predsednik2 = mapper.Map<Predsednik>(predsednik);
                PredsednikConfirmation confirmation = predsednikRepository.UpdatePredsednik(predsednik2);
                return Ok(mapper.Map<PredsednikDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}