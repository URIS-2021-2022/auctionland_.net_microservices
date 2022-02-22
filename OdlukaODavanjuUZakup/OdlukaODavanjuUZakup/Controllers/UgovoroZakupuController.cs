using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OdlukaODavanjuUZakup.Data;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OdlukaODavanjuUZakup.Controllers
{
    /// <summary>
    /// Controler za ugovor o zakupu entitet
    /// </summary>
    [ApiController]
    [Route("api/UgovorOZakupu")]
    [Produces("application/json", "application/xml")]
    public class UgovoroZakupuController : ControllerBase
    {
        private readonly IUgovoroZakupuRepository ugovoroZakupuRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        /// <summary>
        /// 
        /// Konstruktor
        /// </summary>
        /// <param name="ugovoroZakupuRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public UgovoroZakupuController(IUgovoroZakupuRepository ugovoroZakupuRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ugovoroZakupuRepository = ugovoroZakupuRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraca listu svih ugovora o zakupu
        /// </summary>
        /// <param name="zavodni_broj">Prosledjeni parametar po kome moze da filtrira</param>
        /// <returns></returns>
        /// <response code="200">Uspesno vraca sve</response>
        /// <response code="204">Nije pronasao nijedan</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UgovoroZakupuDto>> GetUgovori(string zavodni_broj)
        {
            var ugovori = ugovoroZakupuRepository.GetUgovoriOZakupu(zavodni_broj);

            if (ugovori == null || ugovori.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<UgovoroZakupuDto>>(ugovori));
        }
        /// <summary>
        /// Vraca ugovor za prosledjeni ID
        /// </summary>
        /// <param name="UgovoroZakupuID">prosledjeni ID ugovora</param>
        /// <returns></returns>
        /// <response code="200">Vraca uspesno ugovor</response>
        /// <response code = "404">Nije pronasao ugovor sa tim ID</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{UgovoroZakupuID}")]
        public ActionResult<UgovoroZakupuDto> GetUgovor(Guid UgovoroZakupuID)
        {
            var ugovor = ugovoroZakupuRepository.GetUgovoriOZakupuById(UgovoroZakupuID);
            if (ugovor == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UgovoroZakupuDto>(ugovor));
        }
        /// <summary>
        /// KReira se novi ugovor o zakupu u bazi
        /// </summary>
        /// <param name="ugovoroZakupu"></param>
        /// <returns></returns>
        /// <response code ="201">Uspesno je kreiran novi ugovor</response>
        /// <response code = "500">Doslo je do greske prilikom kreiranja</response>
        /// <response code = "400">Poslat je nevalidan zahtev za kreiranje</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UgovoroZakupuConfirmationDto> CreateUgovorOZakupu([FromBody] UgovoroZakupuCreationDto ugovoroZakupu)
        {
            try
            {
                bool modelValid = ValidateUgovoroZakupu(ugovoroZakupu);

                if (!modelValid)
                {
                    return BadRequest("Datum za rok nije validan");
                }
                var ugovoroZakupuEntity = mapper.Map<UgovoroZakupu>(ugovoroZakupu);
                var confirmation = ugovoroZakupuRepository.CreateUgovorOZakupu(ugovoroZakupuEntity);
                ugovoroZakupuRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("getUgovori", "UgovorOZakupu", new { UgovoroZakupuID = confirmation.UgovoroZakupuID });
                return Created(location, mapper.Map<UgovoroZakupuConfirmationDto>(confirmation));
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateUgovoroZakupu(UgovoroZakupuCreationDto ugovoroZakupu)
        {
            return true;
        }
        /// <summary>
        /// Brise se ugovor o zakupu sa prosledjenim ID
        /// </summary>
        /// <param name="UgovoroZakupuID">Prosledjeni ID</param>
        /// <returns></returns>
        /// <response code="200">Uspesno je obrisan ugovor</response>
        /// <response code ="404">Nije pronadjen ugovor sa tim ID</response>
        /// <response code ="500">Doslo je do greske na serveru</response>
        /// <response code ="204">Obrisan je i vraca prazan body</response>
        [HttpDelete("{UgovoroZakupuID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteUgovoroZakupu (Guid UgovoroZakupuID)
        {
            {
                try
                {
                    var ugovor = ugovoroZakupuRepository.GetUgovoriOZakupuById(UgovoroZakupuID);

                    if (ugovor == null)
                    {
                        return NotFound();

                    }
                    ugovoroZakupuRepository.DeleteUgovorOZakupu(UgovoroZakupuID);
                    ugovoroZakupuRepository.SaveChanges();
                    return NoContent();
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
                }

            }
        }
        /// <summary>
        /// Azuriranje ugovora
        /// </summary>
        /// <param name="ugovor"></param>
        /// <returns></returns>
        ///  <response code ="201">Uspesno je azuriran novi ugovor</response>
        /// <response code = "500">Doslo je do greske prilikom azuriranja</response>
        /// <response code = "404">Nije pronadjen za update</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UgovoroZakupuConfirmationDto> UpdateUgovoroZakupu(UgovoroZakupuUpdateDto ugovor)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if  (ugovoroZakupuRepository.GetUgovoriOZakupuById(ugovor.UgovoroZakupuID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                UgovoroZakupu ugovor2 = mapper.Map<UgovoroZakupu>(ugovor);
                UgovoroZakupuConfirmation confirmation = ugovoroZakupuRepository.UpdateUgovorOZakupu(ugovor2);
                return Ok(mapper.Map<UgovoroZakupuDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        [HttpOptions]
        public IActionResult GetUgovoroZakupuOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
