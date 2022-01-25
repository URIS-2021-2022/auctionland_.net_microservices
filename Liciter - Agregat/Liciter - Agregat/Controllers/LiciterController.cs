﻿using Liciter___Agregat.Data;
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

        public LiciterController(ILiciterRepository liciterRepository, LinkGenerator linkGenerator)
        {
            this.liciterRepository = liciterRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<LiciterModel>> GetLiciteri(string JMBG_MaticniBroj)
        {
            List<LiciterModel> liciteri = liciterRepository.GetLiciters(/*JMBG_MaticniBroj*/);
            if (liciteri == null || liciteri.Count == 0)
            {
                return NoContent();
            }
            return Ok(liciteri);
        }

        [HttpGet("{liciterId}")]
        public ActionResult<LiciterModel> GetLiciterbyId(Guid liciterId)
        {
            LiciterModel liciterModel = liciterRepository.GetLiciterById(liciterId);
            if (liciterModel == null)
            {
                return NotFound();
            }
            return Ok(liciterModel);
        }

        [HttpPost]
        public ActionResult<LiciterConfirmation> CreateLiciter([FromBody] LiciterModel liciter)
        {
            try
            {


                LiciterConfirmation confirmation = liciterRepository.CreateLiciter(liciter);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetLiciter", "Liciter", new { liciterId = confirmation.LiciterId });
                return Created(location, confirmation);
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
    }
}
