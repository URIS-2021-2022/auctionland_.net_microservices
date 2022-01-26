using AutoMapper;
using Liciter___Agregat.Data;
using Liciter___Agregat.DTOs.Liciter;
using Liciter___Agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    class LiciterController : ControllerBase
    {
        private readonly ILiciterRepository liciterRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public LiciterController(ILiciterRepository liciterRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.liciterRepository = liciterRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<LiciterDto>> GetLiciteri(string JMBG_MaticniBroj)
        {
            List<LiciterModel> liciteri = liciterRepository.GetLiciters(/*JMBG_MaticniBroj*/);
            if (liciteri == null || liciteri.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<LiciterDto>>(liciteri));
        }

        [HttpGet("{liciterId}")]
        public ActionResult<LiciterDto> GetLiciterbyId(Guid liciterId)
        {
            LiciterModel liciterModel = liciterRepository.GetLiciterById(liciterId);
            if (liciterModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<LiciterDto>(liciterModel));
        }

        [HttpPost]
        public ActionResult<LiciterConfirmationDto> CreateLiciter([FromBody] LiciterCreationDto liciter)
        {
            try
            {

                LiciterModel liciter2 = mapper.Map<LiciterModel>(liciter);
                LiciterConfirmation confirmation = liciterRepository.CreateLiciter(liciter2);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetLiciter", "Liciter", new { liciterId = confirmation.LiciterId });
                return Created(location, mapper.Map<LiciterConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpDelete("{liciterId}")]
        public IActionResult DeleteLiciter(Guid liciterId)
        {
            try
            {
                LiciterModel liciterModel = liciterRepository.GetLiciterById(liciterId);
                if (liciterModel == null)
                {
                    return NotFound();
                }
                liciterRepository.DeleteLiciter(liciterId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<LiciterConfirmationDto> UpdateKupac(LiciterUpdateDto liciter)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (liciterRepository.GetLiciterById(liciter.LiciterId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                LiciterModel liciterModel = mapper.Map<LiciterModel>(liciter);
                LiciterConfirmation confirmation = liciterRepository.UpdateLiciter(liciterModel);
                return Ok(mapper.Map<LiciterConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
    }
}
