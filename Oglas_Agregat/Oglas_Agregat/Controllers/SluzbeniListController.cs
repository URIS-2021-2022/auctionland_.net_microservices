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
    [Route("api/SluzbeniList")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;

        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator link, IMapper mapper)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.link = link;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<SluzbeniListDto>> GetSluzbeniListovi(int BrojLista)
        {
            var sluzbeniListovi = sluzbeniListRepository.GetSluzbeniListovi(BrojLista);
            if (sluzbeniListovi == null || sluzbeniListovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<SluzbeniListDto>>(sluzbeniListovi));
        }

        [HttpGet("{sluzbeniListId}")]
        public ActionResult<SluzbeniListDto> GetSluzbeniListById(Guid sluzbeniListId)
        {
            var sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId);

            if (sluzbeniList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<SluzbeniListDto>(sluzbeniList));
        }

        [HttpPost]
        public ActionResult<SluzbeniListConfirmationDto> CreateSluzbeniList([FromBody] SluzbeniListCreateDto sluzbeniList)
        {
            try
            {
                var sluzbeniListEntity = mapper.Map<SluzbeniList>(sluzbeniList);

                var confirmation = sluzbeniListRepository.CreateSluzbeniList(sluzbeniListEntity);

                string location = link.GetPathByAction("GetSluzbeniListovi", "SluzbeniList", new { sluzbeniListId = confirmation.SluzbeniListId });
                return Created(location, mapper.Map<SluzbeniListConfirmationDto>(confirmation));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while creating an object.");
            }
        }

        [HttpDelete("{sluzbeniListId}")]
        public IActionResult DeleteSluzbeniList(Guid sluzbeniListId)
        {
            try
            {
                var sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId);

                if (sluzbeniList == null)
                {
                    return NotFound();
                }

                sluzbeniListRepository.DeleteSluzbeniList(sluzbeniListId);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while deleting the object.");
            }
        }

        [HttpPut]
        public ActionResult<SluzbeniListConfirmationDto> UpdateSluzbeniList(SluzbeniListUpdateDto sluzbeniList)
        {
            try
            {
                if (sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId) == null)
                {
                    return NotFound();
                }

                SluzbeniList sluzbeniListEntity = mapper.Map<SluzbeniList>(sluzbeniList);
                SluzbeniListConfirmation confirmation = sluzbeniListRepository.UpdateSluzbeniList(sluzbeniListEntity);
                return Ok(mapper.Map<SluzbeniListConfirmationDto>(confirmation));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }

        [HttpOptions]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
