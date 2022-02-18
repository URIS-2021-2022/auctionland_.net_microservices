using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Oglas_Agregat.Data;
using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Controllers
{
    [ApiController]
    [Route("api/Oglasi")]
    public class OglasController : ControllerBase //ControllerBase za neke dodatne funkcionalnosti npr manipulacija status kodovima 
    {
        private readonly IOglasRepository oglasRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;

        public OglasController(IOglasRepository oglasRepository, LinkGenerator link, IMapper mapper) //kad se kreira objekat kontrolera mora da se prosledi nesto sto implementira ovaj interfejs, kod mene oglasConfirmation
        {
            this.oglasRepository = oglasRepository;
            this.link = link;
            this.mapper = mapper;
        }


        [HttpGet]
        [HttpHead]
        public ActionResult<List<OglasDto>> GetOglasi(DateTime DatumObjave)
        {
            //treba da zavisimo od apstrakcije a ne od konkretne implementacije, ne treba da pravimo instancu klase i direktno pozivamo metodu sto nije dobro, zato smo pravili interfejs
            var oglasi = oglasRepository.GetOglasi(DatumObjave);
            if (oglasi == null || oglasi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OglasDto>>(oglasi));
        }

        [HttpGet("{oglasId}")]
        public ActionResult<OglasDto> GetOglasById(Guid oglasId)
        {
            var oglas = oglasRepository.GetOglasById(oglasId);

            if (oglas == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OglasDto>(oglas));
        }

        [HttpPost]
        public ActionResult<OglasConfirmationDto> CreateOglas([FromBody] OglasCreateDto oglas)
        {
            try
            {
                var oglasEntity = mapper.Map<Oglas>(oglas);

                var confirmation = oglasRepository.CreateOglas(oglasEntity);

                string location = link.GetPathByAction("GetOglasi", "Oglas", new { oglasId = confirmation.OglasId });
                return Created(location, mapper.Map<OglasConfirmationDto>(confirmation));

            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while creating the object.");

            }
        }

        [HttpDelete("{oglasId}")]
        public IActionResult DeleteOglas(Guid oglasId)
        {
            try
            {
                var oglas = oglasRepository.GetOglasById(oglasId);

                if (oglas == null)
                {
                    return NotFound();
                }

                oglasRepository.DeleteOglas(oglasId);
                return NoContent(); //e sve je okej proslo ali ja ne vracam nikakav sadrzaj - 200

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while deleting the object.");
            }
        }

        [HttpPut]
        public ActionResult<OglasConfirmationDto> UpdateOglas(OglasUpdateDto oglas)
        {
            try
            {
                if (oglasRepository.GetOglasById(oglas.OglasId) == null)
                {
                    return NotFound();
                }

            Oglas oglasEntity = mapper.Map<Oglas>(oglas);
            OglasConfirmation confirmation = oglasRepository.UpdateOglas(oglasEntity);
            return Ok(mapper.Map<OglasConfirmationDto>(confirmation));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }


        [HttpOptions]
        public IActionResult GetOglasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }



    }
}
