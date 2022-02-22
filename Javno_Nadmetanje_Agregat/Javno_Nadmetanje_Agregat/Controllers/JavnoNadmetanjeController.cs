using AutoMapper;
using Javno_Nadmetanje_Agregat.Data;
using Javno_Nadmetanje_Agregat.Entities;
using Javno_Nadmetanje_Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Controllers
{
    /// <summary>
    /// Predstavlja kontroler oglasa
    /// </summary>
    [ApiController]
    [Route("api/javno-nadmetanje")]
    [Produces("application/json")]
    public class JavnoNadmetanjeController : ControllerBase
    {
        private readonly IJavnoNadmetanjeRepository javnoNadmetanjeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;


        public JavnoNadmetanjeController(
            IJavnoNadmetanjeRepository javnoNadmetanjeRepository,
            LinkGenerator linkGenerator, IMapper mapper,
            ILoggerService loggerService
            )
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca listu svih javnih nadmetanja
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Vraca listu svih javnih nadmetanja</response>
        /// <response code="204">Nema javnih nadmetanja</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnoNadmetanje()
        {
            List<JavnoNadmetanje> javnaNadmetanja = javnoNadmetanjeRepository.GetJavnoNadmetanjeList();
            if (javnaNadmetanja == null || javnaNadmetanja.Count == 0)
            {

                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista javnih nadmetanja je prazna ili null.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista javnih nadmetanja je uspesno vracena!");
            return Ok(mapper.Map<List<JavnoNadmetanjeDto>>(javnaNadmetanja));
        }

        /// <summary>
        /// Vraca javno nadmetanje sa trazenim ID-em
        /// </summary>
        /// <param name="javnoNadmetanjeId">Sifra javnog nadmetanja</param>
        /// <returns></returns>
        /// <response code="200">Vraca trazeno javno nadmetanje</response>
        /// <response code="404">Javno nadmetanje nije pronadjeno</response>
        [HttpGet("{javnoNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<JavnoNadmetanjeDto> GetJavnoNadmetanjeById(Guid javnoNadmetanjeId)
        {
            JavnoNadmetanje javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
            if (javnoNadmetanje == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Javno nadmetanje sa datim id-em nije pronadjeno.");
                return NotFound();
            }

            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Javno nadmetanje sa datim id-em je uspesno vracena!");
            return Ok(mapper.Map<JavnoNadmetanjeDto>(javnoNadmetanje));
        }

        /// <summary>
        /// Upis novog javnog nadmetanja
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Vraca kreirano javno nadmetanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JavnoNadmetanjeConfirmationDto> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeCreateDto javnoNadmetanjeDto)
        {
            try
            {
                JavnoNadmetanje javnoNadmetanje = mapper.Map<JavnoNadmetanje>(javnoNadmetanjeDto);
                JavnoNadmetanjeConfirmationDto confirmation = javnoNadmetanjeRepository.CreateJavnoNadmetanje(javnoNadmetanje);
                string location = linkGenerator.GetPathByAction("GetJavnoNadmetanjeById", "JavnoNadmetanje", new { javnoNadmetanjeId = confirmation.JavnoNadmetanjeId });

                loggerService.Log(LogLevel.Information, "PostStatus", "Javno nadmetanje je uspesno napravljeno!");
                return Created(location, mapper.Map<JavnoNadmetanjeConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Javno nadmetanje nije kreirano, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Brisanje javnog nadmetanja sa trazenim ID-em
        /// </summary>
        /// <param name="javnoNadmetanjeId">Sifra javnog nadmetanja</param>
        /// <returns></returns>
        /// <response code="200">Vraca izbrisano javno nadmetanje</response>
        /// <response code="404">Javno nadmetanje nije pronadjeno</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja javnog nadmetanja</response>
        [HttpDelete("{javnoNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteJavnoNadmetanje(Guid javnoNadmetanjeId)
        {
            try
            {
                JavnoNadmetanje javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
                if (javnoNadmetanje == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Javno nadmetanje sa datim id-em nije pronadjeno.");
                    return NotFound();
                }
                JavnoNadmetanjeConfirmationDto confirmation = javnoNadmetanjeRepository.DeleteJavnoNadmetanje(javnoNadmetanjeId);

                loggerService.Log(LogLevel.Information, "DeleteStatus", "Javno nadmetanje je uspesno obrisano!");
                return Ok(mapper.Map<JavnoNadmetanjeConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Javno nadmetanje nije obrisano, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Azuriranje postojeceg javnog nadmetanja
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Vraca azurirano javno nadmetanje</response>
        /// <response code="404">Javno Nadmetanje nije pronadjeno</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JavnoNadmetanjeConfirmationDto> UpdateJavnoNadmetanje(JavnoNadmetanjeUpdateDto javnoNadmetanjeDto)
        {
            try
            {
                JavnoNadmetanje oldJn = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeDto.JavnoNadmetanjeId);

                if (oldJn == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Javno nadmetanje sa datim id-em nije pronadjeno.");
                    return NotFound();
                }

                JavnoNadmetanje jn = mapper.Map<JavnoNadmetanje>(javnoNadmetanjeDto);

                mapper.Map(jn, oldJn);

                JavnoNadmetanjeConfirmationDto confirmation = javnoNadmetanjeRepository.UpdateJavnoNadmetanje(jn);

                loggerService.Log(LogLevel.Information, "PutStatus", "Javno nadmetanje je uspesno izmenjeno!");
                return Ok(mapper.Map<JavnoNadmetanjeConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Javno nadmetanje nije izmenjeno, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetJavnoNadmetanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            return Ok();
        }


    }
}