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
    /// Predstavlja kontroler tipa javnog nadmetanja
    /// </summary>
    [ApiController]
    [Route("api/tip-javnog-nadmetanja")]
    [Produces("application/json")]
    public class TipJavnogNadmetanjaController : ControllerBase
    {
        private readonly ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public TipJavnogNadmetanjaController(ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.tipJavnogNadmetanjaRepository = tipJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca listu svih tipova javnih nadmetanja
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Vraca listu svih tipova javnih nadmetanja</response>
        /// <response code="204">Nema tipova javnih nadmetanja</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<TipJavnogNadmetanjaDto>> GetTipJavnogNadmetanja()
        {
            List<TipJavnogNadmetanja> tipovi = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaList();
            if (tipovi == null || tipovi.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllStatus", "Lista tipova javnih nadmetanja je prazna ili null.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista tipova javnih nadmetanja je uspesno vracena!");
            return Ok(mapper.Map<List<TipJavnogNadmetanjaDto>>(tipovi));
        }

        /// <summary>
        /// Vraca tip javnog nadmetanja sa trazenim ID-em
        /// </summary>
        /// <param name="tipJavnogNadmetanjaId">Sifra tipa javnog nadmetanja</param>
        /// <returns></returns>
        /// <response code="200">Vraca trazeni tip javnog nadmetanja</response>
        /// <response code="404">Tip javnog nadmetanja nije pronadjen</response>
        [HttpGet("{tipJavnogNadmetanjaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TipJavnogNadmetanjaDto> GetTipJavnogNadmetanjaById(Guid tipJavnogNadmetanjaId)
        {
            TipJavnogNadmetanja tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaId);
            if (tipJavnogNadmetanja == null)
            {
                loggerService.Log(LogLevel.Warning, "GetByIdStatus", "Tip javnog nadmetanja sa datim id-em nije pronadjen.");
                return NotFound();
            }

            loggerService.Log(LogLevel.Information, "GetByIdStatus", "Tip javnog nadmetanja sa datim id-em je uspesno vracen!");
            return Ok(mapper.Map<TipJavnogNadmetanjaDto>(tipJavnogNadmetanja));
        }

        /// <summary>
        /// Upis novog tipa javnog nadmetanja
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Vraca kreiran tip javnog nadmetanja</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja tipa javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipJavnogNadmetanjaConfirmationDto> CreateTipJavnogNadmetanja([FromBody] TipJavnogNadmetanjaCreateDto tipJavnogNadmetanjaDto)
        {
            try
            {
                TipJavnogNadmetanja tipJavnogNadmetanja = mapper.Map<TipJavnogNadmetanja>(tipJavnogNadmetanjaDto);
                TipJavnogNadmetanjaConfirmationDto confirmation = tipJavnogNadmetanjaRepository.CreateTipJavnogNadmetanja(tipJavnogNadmetanja);
                string location = linkGenerator.GetPathByAction("GetTipJavnogNadmetanjaById", "TipJavnogNadmetanja", new { tipJavnogNadmetanjaId = confirmation.TipJavnogNadmetanjaId });

                loggerService.Log(LogLevel.Information, "PostStatus", "Tip javnog nadmetanja je uspesno napravljen!");
                return Created(location, mapper.Map<TipJavnogNadmetanjaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PostStatus", "Tip javnog nadmetanja nije kreiran, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Brisanje tipa javnog nadmetanja sa trazenim ID-em
        /// </summary>
        /// <param name="tipJavnogNadmetanjaId">Sifra tipa javnog nadmetanja</param>
        /// <returns></returns>
        /// <response code="200">Vraca izbrisan tip javnog nadmetanja</response>
        /// <response code="404">Tip javnog nadmetanja nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja</response>
        [HttpDelete("{tipJavnogNadmetanjaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaId)
        {
            try
            {
                TipJavnogNadmetanja tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaId);
                if (tipJavnogNadmetanja == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteStatus", "Tip javnog nadmetanja sa datim id-em nije pronadjen.");
                    return NotFound();
                }
                TipJavnogNadmetanjaConfirmationDto confirmation = tipJavnogNadmetanjaRepository.DeleteTipJavnogNadmetanja(tipJavnogNadmetanjaId);
                
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Tip javnog nadmetanja je uspesno obrisan!");
                return Ok(mapper.Map<TipJavnogNadmetanjaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "DeleteStatus", "Tip javnog nadmetanja nije obrisan, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Azuriranje postojeceg tipa javnog nadmetanja
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Vraca azuriran tip javnog nadmetanja</response>
        /// <response code="404">Tip javnog nadmetanja nije pronadjen</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipJavnogNadmetanjaConfirmationDto> UpdateTipJavnogNadmetanja(TipJavnogNadmetanjaUpdateDto tipJavnogNadmetanjaDto)
        {
            try
            {
                TipJavnogNadmetanja oldTjn = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaDto.TipJavnogNadmetanjaId);

                if (oldTjn == null)
                {
                    loggerService.Log(LogLevel.Warning, "PutStatus", "Tip javnog nadmetanja sa datim id-em nije pronadjen.");
                    return NotFound();
                }

                TipJavnogNadmetanja Tjn = mapper.Map<TipJavnogNadmetanja>(tipJavnogNadmetanjaDto);

                mapper.Map(Tjn, oldTjn);

                TipJavnogNadmetanjaConfirmationDto confirmation = tipJavnogNadmetanjaRepository.UpdateTipJavnogNadmetanja(Tjn);

                loggerService.Log(LogLevel.Information, "PutStatus", "Tip javnog nadmetanja je uspesno izmenjen!");
                return Ok(mapper.Map<TipJavnogNadmetanjaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Warning, "PutStatus", "Tip javnog nadmetanja nije izmenjen, doslo je do greske.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetTipJavnogNadmetanjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            return Ok();
        }
    }
}
