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
    [Route("api/zakupnine")]
    public class UplataZakupnineController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IUplataZakupnineRepository uplataZakupnineRepository;

        public UplataZakupnineController(IUplataZakupnineRepository uplataZakupnineRepository, LinkGenerator linkGenerator, IMapper mapper)

        {
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.uplataZakupnineRepository = uplataZakupnineRepository;
        }
        [HttpGet]
        public ActionResult<List<UplataZakupnineDto>> getUplate(string broj_racuna)
        {
            var uplate = uplataZakupnineRepository.GetUplateZakupnine(broj_racuna);

            if (uplate == null || uplate.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<UplataZakupnineDto>>(uplate));
        }

        [HttpGet("{UplataZakupnineID}")]
        public ActionResult<UplataZakupnineDto> getUplata(Guid uplataZakupnineID)
        {
            var uplata = uplataZakupnineRepository.GetUplataZakupnineById(uplataZakupnineID);

            if (uplata == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UplataZakupnineDto>(uplata));
        }
        [HttpPost]
        public ActionResult<UplataZakupnineConfirmationDto> CreateUplataZakupnine([FromBody] UplataZakupnineCreationDto uplataZakupnine)
        {
            try
            {
                bool modelValid = ValidateUplataZakupnine(uplataZakupnine);

                if (!modelValid)
                {
                    return BadRequest("Iznos uplate je isuvise mali");

                }

                var uplataZakupnineEntity = mapper.Map<UplataZakupnine>(uplataZakupnine); 
                var confirmation = uplataZakupnineRepository.CreateUplataZakupnine(uplataZakupnineEntity);
                string location = linkGenerator.GetPathByAction("GetUplate", "UplataZakupnine", new { uplataZakupnineID = confirmation.UplataZakupnineID });
                return Created(location, mapper.Map<UplataZakupnineConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        private bool ValidateUplataZakupnine(UplataZakupnineCreationDto uplataZakupnine)
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
        [HttpOptions]
        public IActionResult GetUplataZakupnineOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
