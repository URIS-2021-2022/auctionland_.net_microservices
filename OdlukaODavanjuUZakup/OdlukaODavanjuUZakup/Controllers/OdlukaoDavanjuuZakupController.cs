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
    [ApiController]
    [Route("api/Odluke")]
    public class OdlukaoDavanjuuZakupController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository;

        public OdlukaoDavanjuuZakupController(IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository, LinkGenerator linkGenerator, IMapper mapper)

        {
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.odlukaoDavanjuuZakupRepository = odlukaoDavanjuuZakupRepository;
        }
        [HttpGet]
        public ActionResult<List<OdlukaoDavanjuuZakupDto>> GetOdluke()
        {
            var odluke = odlukaoDavanjuuZakupRepository.GetOdluke();
            if (odluke == null || odluke.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OdlukaoDavanjuuZakupDto>>(odluke));
        }
        [HttpGet("OdlukaoDavanjuuZakupID")]
        public ActionResult<OdlukaoDavanjuuZakupDto> getOdluka (Guid odlukaoDavanjuuZakupID)
        {
            var odluka = odlukaoDavanjuuZakupRepository.GetOdlukaById(odlukaoDavanjuuZakupID);
            if (odluka == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OdlukaoDavanjuuZakupDto>(odluka));
         }
        [HttpPost]
        public ActionResult<OdlukaoDavanjuuZakupDto> createOdluka([FromBody] OdlukaoDavanjuuZakupDto odlukaoDavanjuuZakup)
        {
            try
            {
                bool modelValid = ValidateOdlukaoDavanjuuZakup(odlukaoDavanjuuZakup);

                if (!modelValid)
                {
                    return BadRequest("Ne valja");
                }
                var odlukaoDavanjuuZakupEntity = mapper.Map<OdlukaoDavanjuuZakup>(odlukaoDavanjuuZakup);
                var confirmation = odlukaoDavanjuuZakupRepository.CreateOdluka(odlukaoDavanjuuZakupEntity);
                var location = linkGenerator.GetPathByAction("GetOdluke", "OdlukaoDavanjuuZakup", new { odlukaoDavanjuuZakupID = confirmation.OdlukaoDavanjuuZakupID });
                return Created(location, mapper.Map<OdlukaoDavanjuuZakupConfirmationDto>(confirmation));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateOdlukaoDavanjuuZakup(OdlukaoDavanjuuZakupDto odlukaoDavanjuuZakup)
        {
            return true;
            // Nemam adekvatni uslov
        }
        [HttpDelete]
        public IActionResult deleteOdluka(Guid odlukaoDavanjuuZakupID)
        {
            try
            {
                var odluka = odlukaoDavanjuuZakupRepository.GetOdlukaById(odlukaoDavanjuuZakupID);
                if (odluka == null)
                {
                    return NotFound();
                }
                odlukaoDavanjuuZakupRepository.DeleteOdluka(odlukaoDavanjuuZakupID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        [HttpPut]
        public ActionResult<OdlukaoDavanjuuZakupConfirmationDto> UpdateOdlukaoDavanjuuZakup(OdlukaoDavanjuuZakupUpdateDto odluka)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (odlukaoDavanjuuZakupRepository.GetOdlukaById(odluka.OdlukaoDavanjuuZakupID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                OdlukaoDavanjuuZakup odluka2 = mapper.Map<OdlukaoDavanjuuZakup>(odluka);
                OdlukaoDavanjuuZakupConfirmation confirmation = odlukaoDavanjuuZakupRepository.UpdateOdluka(odluka2);
                return Ok(mapper.Map<OdlukaoDavanjuuZakupDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
