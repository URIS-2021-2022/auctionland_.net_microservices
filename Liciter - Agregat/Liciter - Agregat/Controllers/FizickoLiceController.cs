using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.FizickoLice;
using Liciter___Agregat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Controllers
{
    [Route("api/fizickolice")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class FizickoLiceController : ControllerBase
    {
        public readonly IFizickoLiceRepository fizickoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        
        public FizickoLiceController(IFizickoLiceRepository fizickoLiceRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.fizickoLiceRepository = fizickoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sva fizička lica iz liste
        /// </summary>
        /// <param name="JMBG"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<FizickoLiceDto>> GetFizickaLicas(string JMBG)
        {
            List<FizickoLiceModel> lica = fizickoLiceRepository.GetFizickaLicas(JMBG);
            if (lica == null || lica.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<FizickoLiceDto>>(lica));
        }

        /// <summary>
        /// Vraća fizičko lice na osnovu id-a
        /// </summary>
        /// <param name="fizickoLiceId"></param>
        /// <returns></returns>
        ///  [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{fizickoLiceId}")]
        public ActionResult<FizickoLiceDto> GetFizickoLicebyId(Guid fizickoLiceId)
        {
            FizickoLiceModel fizickoLiceModel = fizickoLiceRepository.GetFizickoLiceById(fizickoLiceId);
            if (fizickoLiceModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<FizickoLiceDto>(fizickoLiceModel));
        }

        /// <summary>
        /// Dodaje novo fizičko lice u listu
        /// </summary>
        /// <param name="fizickoLice"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FizickoLiceConfirmationDto> CreateFizickoLice([FromBody] FizickoLiceCreationDto fizickoLice)
        {
            try
            {

                FizickoLiceModel lice = mapper.Map<FizickoLiceModel>(fizickoLice);
                FizickoLiceConfirmation confirmation = fizickoLiceRepository.CreateFizickoLice(lice);
                fizickoLiceRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetFizickoLicebyId", "FizickoLice", new { fizickoLiceId = confirmation.FizickoLiceId });
                return Created(location, mapper.Map<FizickoLiceConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error " + ex.Message);
            }

        }

        /// <summary>
        /// Briše fizičko lice sa prosledjenim id-em iz liste
        /// </summary>
        /// <param name="fizickoLiceId"></param>
        /// <returns>Potvrdu o modifikovanom fizickom licu</returns>
        ///  <response code="200">Vraća ažurirano fizičko lice</response>
        /// <response code="400">Fizičko lice koje se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja fizičkog lica</response>
        [HttpDelete("{fizickoLiceId}")]
        public IActionResult DeleteFizickoLice(Guid fizickoLiceId)
        {
            try
            {
                FizickoLiceModel fizickoLiceModel = fizickoLiceRepository.GetFizickoLiceById(fizickoLiceId);
                if (fizickoLiceModel == null)
                {
                    return NotFound();
                }
                fizickoLiceRepository.DeleteFizickoLice(fizickoLiceId);
                fizickoLiceRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vrši izmenu nad fizičkim licem koji se prosledio u body-u
        /// </summary>
        /// <param name="fizickoLice"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Fizičko lice uspešno obrisano</response>
        /// <response code="404">Nije pronađeno fizičko lice za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja fizičkog lica</response>
        [HttpPut]
        public ActionResult<FizickoLiceConfirmationDto> UpdateFizickoLice(FizickoLiceUpdateDto fizickoLice)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (fizickoLiceRepository.GetFizickoLiceById(fizickoLice.FizickoLiceId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                FizickoLiceModel fizickoLiceModel = mapper.Map<FizickoLiceModel>(fizickoLice);
                FizickoLiceConfirmation confirmation = fizickoLiceRepository.UpdateFizickoLice(fizickoLiceModel);
                fizickoLiceRepository.SaveChanges();
                return Ok(mapper.Map<FizickoLiceConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

            
        }
    }
}
