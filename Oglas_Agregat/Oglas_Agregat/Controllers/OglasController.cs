using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Oglas_Agregat.Data;
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

        public OglasController(IOglasRepository oglasRepository, LinkGenerator link) //kad se kreira objekat kontrolera mora da se prosledi nesto sto implementira ovaj interfejs, kod mene oglasConfirmation
        {
            this.oglasRepository = oglasRepository;
            this.link = link;
        }


        //ja hocu da napisem jednu akciju koja ce mi vratiti sve oglase
        [HttpGet]
        public ActionResult GetOglasi(DateTime DatumObjave)
        {
            //treba da zavisimo od apstrakcije a ne od konkretne implementacije, ne treba da pravimo instancu klase i direktno pozivamo metodu sto nije dobro, zato smo pravili interfejs
            var oglasi = oglasRepository.GetOglasi(DatumObjave);
            if (oglasi == null || oglasi.Count == 0)
            {
                return NoContent();
            }
            return Ok(oglasi);
        }

        [HttpGet("{oglasId}")]
        public ActionResult<OglasModel> GetOglasById(Guid oglasId)
        {
            OglasModel oglasModel = oglasRepository.GetOglasById(oglasId);

            if (oglasModel == null)
            {
                return NotFound();
            }
            return Ok(oglasModel);
        }

        [HttpPost]
        public ActionResult<OglasModel> CreateOglas([FromBody] OglasModel oglas) //pogledaj u body i izvuci model
        {
            try
            {
                OglasConfirmation confirmation = oglasRepository.CreateOglas(oglas);
                string location = link.GetPathByAction("GetOglasi", "Oglas", new { oglasId = confirmation.OglasId }); //dobar api treba da vrati gde se novokreirani resurs nalazi
                return Created(location, confirmation);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while creating an object.");
            }
        }

        [HttpDelete("{oglasId}")]
        public ActionResult DeleteOglas(Guid oglasId)
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
        public ActionResult<OglasConfirmation> UpdateOglas(OglasModel oglas)
        {
            try
            {
                if (oglasRepository.GetOglasById(oglas.OglasId) == null)
                { 
                    return NotFound();
                }

                return Ok(oglasRepository.UpdateOglas(oglas));


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }

/*        [HttpOptions]
        public IActionResult GetOglasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }*/




    }
}
