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
    class PredsednikController : ControllerBase
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