using Program_Agregat.Data;
using Program_Agregat.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Entities;
using Microsoft.AspNetCore.Authorization;


namespace Program_Agregat.Controllers
{

    [ApiController]
    [Route("api/Program")]
    [Authorize]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramRepository programRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        

        public ProgramController(IProgramRepository programRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.programRepository = programRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;

        }

        /// <summary>
        /// Vraca sve programe
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ProgramDto>> GetProgrami()
        {
            var programi = programRepository.GetProgrami();
            if (programi == null || programi.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista programa je prazna ili null");
                return NoContent();
            }
            
            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista programa je uspesno vracena!");
            return Ok(mapper.Map<List<ProgramDto>>(programi));
        }

        /// <summary>
        /// Vraca program na osnovu ID-a
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        [HttpGet("{programId}")]
        public ActionResult<ProgramDto> GetProgramById(Guid programId)
        {
            var programModel = programRepository.GetProgramById(programId);
            if (programModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Program sa tim id-em nije pronadjen");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Program sa zadatim id-em je uspesno vracen!");
            return Ok(mapper.Map<ProgramDto>(programModel));
        }

        /// <summary>
        /// Dodaje novi program u listu
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ProgramConfirmationDto> CreateProgram([FromBody] ProgramCreationDto program)
        {
            try
            {
                ProgramEntity programEntity = mapper.Map<ProgramEntity>(program);
                ProgramConfirmation confirmation = programRepository.CreateProgram(programEntity);
                programRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetProgramById", "Program", new { programId = confirmation.ProgramId });
                loggerService.Log(LogLevel.Information, "PostStatus", "Program je uspesno napravljen!");
                return Created(location, mapper.Map<ProgramConfirmationDto>(confirmation));
            }
            catch
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Program nije kreiran, doslo je do greske!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brise program sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="programId"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Program uspesno obrisan</response>
        /// <response code="404">Nije pronadjen program za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja programa</response>
        [HttpDelete("{programId}")]
        public IActionResult DeleteProgram(Guid programId)
        {
            try
            {
                var programModel = programRepository.GetProgramById(programId);
                if (programModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Program sa tim id-em nije pronadjen");
                    return NotFound();
                }
                programRepository.DeleteProgram(programId);
                programRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Program je uspesno obrisan!");
                return NoContent();
            }
            catch
            {
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Program nije uspesno obrisan!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrsi izmenu programa koji se prosledio u body-u
        /// </summary>
        /// <param name="program"></param>
        /// <returns>Potvrdu o modifikovanom programu</returns>
        /// <response code="200">Vraca azurirani programe</response>
        /// <response code="400">Program koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja programa</response>
        [HttpPut]
        public ActionResult<ProgramConfirmationDto> UpdateProgram(ProgramUpdateDto program)
        {
            try
            {
                if (programRepository.GetProgramById(program.ProgramId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Program sa tim id-em nije pronadjen");
                    return NotFound();
                }
                ProgramEntity program2 = mapper.Map<ProgramEntity>(program);
                ProgramConfirmation confirmation = programRepository.UpdateProgram(program2);
                programRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "PutStatus", "Program je uspesno izmenjen!");
                return Ok(mapper.Map<ProgramConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Doslo je do greske prilikom izmene programa");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}