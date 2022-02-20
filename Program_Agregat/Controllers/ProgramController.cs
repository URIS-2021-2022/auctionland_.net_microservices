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
    class ProgramController : ControllerBase
    {
        private readonly IProgramRepository programRepository;
        private readonly LinkGenerator linkGenerator;

        public ProgramController(IProgramRepository programRepository, LinkGenerator linkGenerator)
        {
            this.programRepository = programRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<ProgramModel>> GetProgrami(int MaksimalnoOgranicenje)
        {
            List<ProgramModel> programi = programRepository.GetProgrami(MaksimalnoOgranicenje);
            if(programi == null || programi.Count == 0)
            {
                return NoContent();
            }
            return Ok(programi)
        }

        [HttpGet("{programId}")]
        public ActionResult<ProgramModel> GetProgramById(Guid programId)
        {
            ProgramModel programModel = programRepository.GetProgramById(programId);
            if (programModel == null)
            {
                return NotFound();
            }
            return Ok(programModel)
        }

        [HttpPost]
        public ActionResult<ProgramConfirmation> CreateProgram([FromBody] ProgramModel program)
        {
            try
            {
                ProgramConfirmation confirmation = programRepository.CreateProgram(program);
                string location = linkGenerator.GetPathByAction("GetProgram", "Program", new { programId = confirmation.ProgramId });
                return Created(location, confirmation);
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
                ProgramModel programModel = programRepository.GetProgramById(programId);
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
    }
}