using Program_Agregat.Data;
using Program_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.linq;
using System.Threading.Tasks;

namespace Program_Agregat.Controllers
{
    [Route("api/[controller]")]
    [apiController]
    class PredlogPlanaController : ControllerBase
    {
        private readonly IPredlogPlanaRepository predlogPlanaRepository;
        private readonly LingGenerator lingGenerator;

        public PredlogPlanaController(IPredlogPlanaRepository predlogPlanaRepository, LinkGenerator linkGenerator)
        {
            this.predlogPlanaRepository = predlogPlanaRepository;
            this.lingGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<PredlogPlanaModel>> GetPredloziPlana(int BrojDokumenta)
        {
            List<PredlogPlanaModel> predloziPlana = predlogPlanaRepository.GetPredloziPlana(BrojDokumenta);
            if (predloziPlana == null || predloziPlana.Count == 0)
            {
                return NoContent();
            }
            return Ok(predloziPlana)
        }

        [HttpGet("{predlogPlanaId}")]
        public ActionResult<PredlogPlanaModel> GetPredlogPlanaById(Guid predlogPlanaId)
        {
            PredlogPlanaModel predlogPlanaModel = predlogPlanaRepository.GetPredlogPlanaById(predlogPlanaId);
            if (predlogPlanaModel == null)
            {
                return NotFound();
            }
            return Ok(predlogPlanaModel)
        }

        [HttpPost]
        public ActionResult<PredlogPlanaConfirmation> CreatePredlogPlana([FromBody] PredlogPlanaModel predlogPlana)
        {
            try
            {
                PredlogPlanaConfirmation confirmation = predlogPlanaRepository.CreatePredlogPlana(predlogPlana);
                string location = lingGenerator.GetPathByAction("GetPredlogPlana", "PredlogPlana", new { predlogPlanaId = confirmation.PredlogPlanaId });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{predlogPlanaId}")]
        public IActionResult DeletePredlogPlana(Guid predlogPlanaId)
        {
            try
            {
                PredlogPlanaModel predlogPlanaModel = predlogPlanaRepository.GetPredlogPlanaById(predlogPlanaId);
                if (predlogPlanaModel == null)
                {
                    return NotFound();
                }
                predlogPlanaRepository.DeletePredlogPlana(predlogPlanaId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}