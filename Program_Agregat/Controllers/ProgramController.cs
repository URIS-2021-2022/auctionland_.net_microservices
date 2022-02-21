using Program_Agregat.Data;
using Program_Agregat.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Entities;

namespace Program_Agregat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    class ProgramController : ControllerBase
    {
        private readonly IProgramRepository programRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProgramController(IProgramRepository programRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.programRepository = programRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ProgramDto>> GetProgrami(string MaksimalnoOgranicenje)
        {
            var programi = programRepository.GetProgrami(MaksimalnoOgranicenje);
            if(programi == null || programi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ProgramDto>>(programi));
        }

        [HttpGet("{programId}")]
        public ActionResult<ProgramDto> GetProgramById(Guid programId)
        {
            var programModel = programRepository.GetProgramById(programId);
            if (programModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProgramDto>(programModel));
        }

        [HttpPost]
        public ActionResult<ProgramConfirmationDto> CreateProgram([FromBody] ProgramCreationDto program)
        {
            try
            {
                var programEntity = mapper.Map<Program>(program);
                var confirmation = programRepository.CreateProgram(programEntity);
                string location = linkGenerator.GetPathByAction("GetProgram", "Program", new { programId = confirmation.ProgramId });
                return Created(location, mapper.Map<ProgramDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{programId}")]
        public IActionResult DeleteProgram(Guid programId)
        {
            try 
            {
                var programModel = programRepository.GetProgramById(programId);
                if(programModel == null)
                {
                    return NotFound();
                }
                programRepository.DeleteProgram(programId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<PredlogPlanaConfirmationDto> UpdatePredlogPlana(PredlogPlanaUpdateDto predlogPlana)
        {
            try
            {
                if (predlogPlanaRepository.GetPredlogPlanaById(predlogPlana.PredlogPlanaId) == null)
                {
                    return NotFound();
                }
                PredlogPlana predlogPlana2 = mapper.Map<PredlogPlana>(predlogPlana);
                PredlogPlanaConfirmation confirmation = predlogPlanaRepository.UpdatePredlogPlana(predlogPlana2);
                return Ok(mapper.Map<PredlogPlanaDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}