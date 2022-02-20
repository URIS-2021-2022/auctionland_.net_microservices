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
    class PredsednikController : ControllerBase
    {
        private readonly IPredsednikRepository predsednikRepository;
        private readonly LinkGenerator linkGenerator;

        public PredsednikController(IPredsednikRepository predsednikRepository, LinkGenerator linkGenerator)
        {
            this.predsednikRepository = predsednikRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<PredsednikModel>> GetPredsednici(string ImePredsednika, string PrezimePredsednika, string EmailPredsednika)
        {
            List<PredsednikModel> predsednici = predsednikRepository.GetPredsednici(string ImePredsednika, string PrezimePredsednika, string EmailPredsednika);
            if (predsednici == null || predsednici.Count == 0)
            {
                return NoContent();
            }
            return Ok(predsednici)
        }

        [HttpGet("{predsednikId}")]
        public ActionResult<PredsednikModel> GetPredsednikById(Guid predsednikId)
        {
            PredsednikModel predsednikModel = predsednikRepository.GetPredsednikById(predsednikId);
            if (predsednikModel == null)
            {
                return NotFound();
            }
            return Ok(predsednikModel)
        }

        [HttpPost]
        public ActionResult<PredsednikConfirmation> CreatePredsednik([FromBody] PredsednikModel predsednik)
        {
            try
            {
                PredsednikConfirmation confirmation = predsednikRepository.CreatePredsednik(predsednik);
                string location = linkGenerator.GetPathByAction("GetPredsednik", "Predsednik", new { predsednikId = confirmation.PredsednikId });
                return Created(location, confirmation);
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
                PredsednikModel predsednikModel = predsednikRepository.GetPredsednikById(predsednikId);
                if (predsednikModel == null)
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
    }
}