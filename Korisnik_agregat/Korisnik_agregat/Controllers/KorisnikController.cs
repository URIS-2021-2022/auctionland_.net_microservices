using AutoMapper;
using Korisnik_agregat.Data;
using Korisnik_agregat.Entities;
using Korisnik_agregat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KorisnikController(IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve korisnike
        /// </summary>
        /// <returns>Listu korisnika</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KorisnikDto>> GetKorisnikList()
        {
            List<Korisnik> korisnikList = korisnikRepository.GetKorisniks();

            if (korisnikList == null || korisnikList.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<KorisnikDto>>(korisnikList));
        }

        /// <summary>
        /// Vraća korisnika po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="korisnikId"></param>
        /// <returns></returns>
        [HttpGet("{korisnikId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<KorisnikDto> GetKorsnikById(Guid korisnikId)
        {
            Korisnik korisnik = korisnikRepository.GetKorisnikById(korisnikId);

            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<KorisnikDto>(korisnik));
        }

        /// <summary>
        /// Kreira novog korisnika
        /// </summary>
        /// <param name="korisnikDto"></param>
        /// <returns>Potvrdu o kreiranom korisniku</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog korisnika
        ///"TipKorisnikaid" : "62D026F1-CAF7-4EAE-BF1A-261A95F7F0BB",
        /// "Ime": "Vlado",
        /// "Prezime": "Cetkovic",
        /// "lozinka" : "vlado123"
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikConfirmationDto> CreateKorisnik([FromBody] KorisnikCreationDto korisnikDto)
        {
            try
            {
                Korisnik korisnik = mapper.Map<Korisnik>(korisnikDto);
                var confirmation = korisnikRepository.CreateKorisnik(korisnik);
                korisnikRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKorisniks", "Korisnik", new { korisnikId = confirmation.KorisnikId});


                return Created(location, mapper.Map<KorisnikConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Menja vrednost zadatog korisnika
        /// </summary>
        /// <param name="korisnikDto"></param>
        /// <returns>Potvrdu o izmenjenom korisniku</returns>
        /// <remarks>    
        /// 
        ///"KorisnikId" : "E8351AD8-DEAC-45EF-B46C-ADDA996397E6",
        ///"TipKorisnikaid" : "62D026F1-CAF7-4EAE-BF1A-261A95F7F0BB",
        ///"Ime": "Nikolina",
        ///"Prezime": "Vuković",
        ///"lozinka" : "kika123"
        ///</remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikConfirmationDto> UpdateKorisnik(KorisnikUpdateDto korisnikDto)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldKorisnik = korisnikRepository.GetKorisnikById(korisnikDto.KorisnikId);
                if (oldKorisnik == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Korisnik korisnikEntity = mapper.Map<Korisnik>(korisnikDto);

                mapper.Map(korisnikEntity, oldKorisnik); //Update objekta koji treba da sačuvamo u bazi                

                korisnikRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<KorisnikDto>(oldKorisnik));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Briše korisnika po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="korisnikId"></param>
        /// <returns>Prazan payload</returns>
        [HttpDelete("{korisnikId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikConfirmationDto> DeleteKorisnik(Guid korisnikId)
        {
            try
            {
                var korisnik = korisnikRepository.GetKorisnikById(korisnikId);

                if (korisnik == null)
                {
                    return NotFound();
                }

                korisnikRepository.DeleteKorisnik(korisnikId);
                korisnikRepository.SaveChanges();
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Prikazuje sve moguće tipove zahteva 
        /// </summary>
        /// <returns>Listu mogućih zahteva</returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            return Ok();
        }
    }
}
