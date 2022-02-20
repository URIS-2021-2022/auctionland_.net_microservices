using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OdlukaODavanjuUZakup.Data;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OdlukaODavanjuUZakup.Controllers
{
    [ApiController]
    [Route("api/zakupnine")]
    public class UplataZakupnineController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IUplataZakupnineRepository uplataZakupnineRepository;

        public UplataZakupnineController(IUplataZakupnineRepository uplataZakupnineRepository, LinkGenerator linkGenerator)

        {
            this.linkGenerator = linkGenerator;
            this.uplataZakupnineRepository = uplataZakupnineRepository;
        }
        [HttpGet]
        public ActionResult<List<UplataZakupnineModel>> getUplate(string broj_racuna)
        {
            var uplate = uplataZakupnineRepository.GetUplateZakupnine(broj_racuna);

            if (uplate == null || uplate.Count == 0)
            {
                return NoContent();
            }
            return Ok(uplate);
        }

        [HttpGet("{UplataZakupnineID}")]
        public ActionResult<UplataZakupnineModel> getUplata(Guid uplataZakupnineID)
        {
            var uplata = uplataZakupnineRepository.GetUplataZakupnineById(uplataZakupnineID);

            if (uplata == null)
            {
                return NotFound();
            }
            return Ok(uplata);
        }
        [HttpPost]
        public ActionResult<UplataZakupnineModel> createUplataZakupnine([FromBody] UplataZakupnineModel uplataZakupnine)
        {
            try
            {
                bool modelValid = ValidateUplataZakupnine(uplataZakupnine);

                if (!modelValid)
                {
                    return BadRequest("Iznos uplate je isuvise mali");

                }
                var confirmation = uplataZakupnineRepository.CreateUplataZakupnine(uplataZakupnine);
                string location = linkGenerator.GetPathByAction("GetUplate", "UplataZakupnine", new { uplataZakupnineID = confirmation.UplataZakupnineID });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateUplataZakupnine(UplataZakupnineModel uplataZakupnine)
        {
            if (uplataZakupnine.iznos < 100)
            {
                return false;
            }
            return true;
        }
        [HttpDelete("{UplataZakupnineID}")]
        public IActionResult deleteUplataZakupnine(Guid uplataZakupnineID)
        {
            try
            {
                var uplata = uplataZakupnineRepository.GetUplataZakupnineById(uplataZakupnineID);

                if (uplata == null)
                {
                    return NotFound();

                }
                uplataZakupnineRepository.DeleteUplataZakupnine(uplataZakupnineID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }

            }

    }
}
