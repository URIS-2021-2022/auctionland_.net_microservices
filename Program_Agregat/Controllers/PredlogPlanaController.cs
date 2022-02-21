using Program_Agregat.Data;
using Program_Agregat.Entities;
using Program_Agregat.Models;
using AutoMapper;
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
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public PredlogPlanaController(IPredlogPlanaRepository predlogPlanaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.predlogPlanaRepository = predlogPlanaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;   
        }

        [HttpGet]
        public ActionResult<List<PredlogPlanaDto>> GetPredloziPlana(int BrojDokumenta)
        {
            List<PredlogPlanaModel> predloziPlana = predlogPlanaRepository.GetPredloziPlana(BrojDokumenta);
            if (predloziPlana == null || predloziPlana.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PredlogPlanaDto>>(predloziPlana));
        }

        [HttpGet("{predlogPlanaId}")]
        public ActionResult<PredlogPlanaDto> GetPredlogPlanaById(Guid predlogPlanaId)
        {
            PredlogPlanaModel predlogPlanaModel = predlogPlanaRepository.GetPredlogPlanaById(predlogPlanaId);
            if (predlogPlanaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PredlogPlanaDto>(predlogPlanaModel));
        }

        [HttpPost]
        public ActionResult<PredlogPlanaConfirmationDto> CreatePredlogPlana([FromBody] PredlogPlanaCreationDto predlogPlana)
        {
            try
            {
                var predlogPlanaEntity = mapper.Map<PredlogPlana>(predlogPlana);
                PredlogPlanaConfirmation confirmation = predlogPlanaRepository.CreatePredlogPlana(predlogPlanaEntity);
                string location = linkGenerator.GetPathByAction("GetPredlogPlana", "PredlogPlana", new { predlogPlanaId = confirmation.PredlogPlanaId });
                return Created(location, mapper.Map<PredlogPlanaDto>(confirmation));
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