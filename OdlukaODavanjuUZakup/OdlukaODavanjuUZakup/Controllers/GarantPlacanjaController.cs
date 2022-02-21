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
    [Route("api/garantPlacanja")]
    public class GarantPlacanjaController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IGarantPlacanjaRepository garantPlacanjaRepository;
        private readonly IMapper mapper;

        public GarantPlacanjaController(IGarantPlacanjaRepository garantPlacanjaRepository, LinkGenerator linkGenerator, IMapper mapper) 
        {
            this.linkGenerator = linkGenerator;
            this.garantPlacanjaRepository = garantPlacanjaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<GarantPlacanjaDto>> GetGaranti()
        {
            var garanti = garantPlacanjaRepository.GetGarantiPlacanja();
            if (garanti == null || garanti.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<GarantPlacanjaDto>>(garanti));
            

        }
        [HttpGet("GarantPlacanjaID")]
        public ActionResult<GarantPlacanjaDto> getGarant(Guid garantPlacanjaID)
        {
            var garant = garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanjaID);
            if (garant == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<GarantPlacanjaDto>(garant));
        }
        [HttpPost]
        public ActionResult<GarantPlacanjaConfirmationDto> CreateGarantPlacanja([FromBody] GarantPlacanjaCreationDto garantPlacanja)
        {
            try
            {
                bool modelValid = ValidateGarantPlacanja(garantPlacanja);

                if (!modelValid)
                {
                    return BadRequest("Ne valja");
                }
                var garantPlacanjaEntity = mapper.Map<GarantPlacanja>(garantPlacanja);
                var confirmation = garantPlacanjaRepository.CreateGarantPlacanja(garantPlacanjaEntity);
                string location = linkGenerator.GetPathByAction("GetGaranti", "GarantPlacanja", new { GarantPlacanjaID = confirmation.GarantPlacanjaID });
                return Created(location, mapper.Map<GarantPlacanjaConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateGarantPlacanja(GarantPlacanjaCreationDto garantPlacanja)
        {
            return true;
            // Nemam adekvatni uslov
        }
        [HttpDelete("GarantPlacanjaID")]
        public IActionResult deleteGarant(Guid garantPlacanjaID)
        {
            try
            {
                var garant = garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanjaID);
                if (garant == null)
                {
                    return NotFound();
                }
                garantPlacanjaRepository.DeleteGarantPlacanja(garantPlacanjaID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        [HttpPut]
        public ActionResult<GarantPlacanjaConfirmationDto> UpdateGarantPlacanja(GarantPlacanjaUpdateDto garantPlacanja)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (garantPlacanjaRepository.GetGarantPlacanjaById(garantPlacanja.GarantPlacanjaID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                GarantPlacanja garantPlacanja2 = mapper.Map<GarantPlacanja>(garantPlacanja);
                GarantPlacanjaConfirmation confirmation = garantPlacanjaRepository.UpdateGarantPlacanja(garantPlacanja2);
                return Ok(mapper.Map<GarantPlacanjaDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
    }
