using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Controllers
{
    /// <summary>
    /// Kontroler za parcele
    /// </summary>
    [ApiController]
    [Route("api/parcela")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parcelaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor kontrolera za parcele
        /// </summary>
        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.parcelaRepository = parcelaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve parcele za zadate filtere.
        /// </summary>
        /// <param name="kultura">Broj resenja (npr. Vrtovi)</param>
        /// <returns>Lista parceli</returns>
        /// <response code="200">Vraća listu parceli</response>
        /// <response code="404">Nije pronađena ni jedna parcela</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ParcelaDto>> GetParcele(string kultura)
        {
            var parcele = parcelaRepository.GetParcele(kultura);

            if (parcele == null || parcele.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ParcelaDto>>(parcele));
        }

        /// <summary>
        /// Vraca jednu parcelu na osnovu ID-ja parcele.
        /// </summary>
        /// <param name="parcelaId">ID parcele</param>
        /// <returns></returns>
        /// <response code="200">Vraca trazenu parcelu</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{parcelaId}")]
        public ActionResult<ParcelaDto> GetParcela(Guid parcelaId)
        {
            var parcela = parcelaRepository.GetParcelaById(parcelaId);

            if (parcela == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ParcelaDto>(parcela));
        }

        /// <summary>
        /// Kreira novu prijavu parcelu.
        /// </summary>
        /// <param name="parcela">Model parcele</param>
        /// <returns>Potvrdu o kreiranoj parceli.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove parcele \
        /// POST /api/parcela \
        /// {     \
        ///     ParcelaId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), \
        ///     Površina = "3000ha", \
        ///     KorisnikParcele = Guid.Parse(""), \
        ///     BrojParcele = "15a", \
        ///     KatastarskaOpstina = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"), \
        ///     BrojListaNepokretnosti = "12345435", \
        ///     Kultura = "Njive", \
        ///     Klasa = "II", \
        ///     Obradivost = "Obradivo", \
        ///     ZasticenaZona = "3", \
        ///     OblikSvojine = "Privatna svojina", \
        ///     Odvodnjavanje = "ostalo", \
        ///     KulturaStvarnoStanje = "Njive", \
        ///     KlasaStvarnoStanje = "II", \
        ///     ObradivostStvarnoStanje = "Ostalo", \
        ///     ZasticenaZonaStvarnoStanje = "4", \
        ///     OdvodnjavanjeStvarnoStanje = "ostalo" \
        ///}
        /// </remarks>
        /// <response code="200">Vraca kreiranu parcelu</response>
        /// <response code="500">Doslo je do greske na serveru</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaConfirmationDto> CreateParcela([FromBody] ParcelaCreationDto parcela)
        {
            try
            {
                ParcelaM parcelaEntity = mapper.Map<ParcelaM>(parcela);
                ParcelaConfirmation confirmation = parcelaRepository.CreateParcela(parcelaEntity);


                var validator = new ParcelaCreationValidator();
                var results = validator.Validate(parcela);

                results.AddToModelState(ModelState, null);


                parcelaRepository.SaveChanges(); 

                string location = linkGenerator.GetPathByAction("GetParcela", "Parcela", new { parcelaId = confirmation.ParcelaId });

                return Created(location, mapper.Map<ParcelaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Azurira jednu parcelu.
        /// </summary>
        /// <param name="parcela">Model parcele koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj parceli.</returns>
        /// <response code="200">Vraca azuriranu parcelu</response>
        /// <response code="400">Parcela koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske na serveru prilikom azuriranja parcele</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> UpdateParcela(ParcelaUpdateDto parcela)
        {
            try
            {
                var oldParcela = parcelaRepository.GetParcelaById(parcela.ParcelaId);
                if (oldParcela == null)
                {
                    return NotFound();
                }
                ParcelaM parcelaEntity = mapper.Map<ParcelaM>(parcela);

                mapper.Map(parcelaEntity, oldParcela);

                var validator = new ParcelaUpdateValidator();
                var results = validator.Validate(parcela);

                results.AddToModelState(ModelState, null);

                parcelaRepository.SaveChanges();
                return Ok(mapper.Map<ParcelaDto>(oldParcela));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne parcele na osnovu ID-ja parcele.
        /// </summary>
        /// <param name="parcelaId">ID parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Parcela uspesno obrisana</response>
        /// <response code="404">Nije pronadjena parcela za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja parcele</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{parcelaId}")]
        public IActionResult DeleteParcela(Guid parcelaId)
        {
            try
            {
                var parcela = parcelaRepository.GetParcelaById(parcelaId);

                if (parcela == null)
                {
                    return NotFound();
                }

                parcelaRepository.DeleteParcela(parcelaId);
                parcelaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa parcelama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        //[AllowAnonymous]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
