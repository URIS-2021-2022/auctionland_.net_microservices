using Licitacija_agregat.Data;
using Licitacija_agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Controllers
{
    [ApiController]
    [Route("api/licitacija")]
    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly LinkGenerator link;

        public LicitacijaController(ILicitacijaRepository licitacijaRepository, LinkGenerator link)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.link = link;
        }

        [HttpGet]
        public ActionResult<List<LicitacijaModel>> GetLicitacijas(DateTime datum)
        {
            var licitacije = licitacijaRepository.GetLicitacijas(datum);
            if (licitacije == null || licitacije.Count == 0)
            {
                return NoContent();
            }
            return Ok(licitacije);
        }

        [HttpGet("{licitacijaId}")]
        public ActionResult<LicitacijaModel> GetLicitacijaById(Guid licitacijaId)
        {
            LicitacijaModel licitacijaModel = licitacijaRepository.GetLicitacijaById(licitacijaId);

            if(licitacijaModel == null)
            {
                return NotFound();
            }
            return Ok(licitacijaModel);
        }

        [HttpPost]
        public ActionResult<LicitacijaModel> CreateLicitacija([FromBody] LicitacijaModel licitacija)
        {
            try
            {
                LicitacijaConfirmation confirmation = licitacijaRepository.CreateLicitacija(licitacija);
                string location = link.GetPathByAction("GetLicitacijas", "Licitacija", new { licitacijaId = confirmation.LicitacijaId });
                return Created(location, confirmation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }
        }

        [HttpDelete]
        public IActionResult DeleteLicitacija(Guid licitacijaId)
        {
            try
            {
                var licitacija = licitacijaRepository.GetLicitacijaById(licitacijaId);

                if(licitacija == null)
                {
                    return NotFound();
                }

                licitacijaRepository.DeleteLicitacija(licitacijaId);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        
    }
}
