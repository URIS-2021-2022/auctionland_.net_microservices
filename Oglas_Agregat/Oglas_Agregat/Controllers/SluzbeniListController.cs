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
    [Route("api/SluzbeniList")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator link;

        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator link)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.link = link;
        }

        [HttpGet]
        public IActionResult GetSluzbeniListovi(int BrojLista)
        {
            var sluzbeniListovi = sluzbeniListRepository.GetSluzbeniListovi(BrojLista);
            if (sluzbeniListovi == null || sluzbeniListovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(sluzbeniListovi);
        }

        [HttpGet("{sluzbeniListId}")]
        public ActionResult<SluzbeniListModel> GetSluzbeniListById(Guid sluzbeniListId)
        {
            SluzbeniListModel sluzbeniListModel = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId);

            if (sluzbeniListModel == null)
            {
                return NotFound();
            }
            return Ok(sluzbeniListModel);
        }

        public ActionResult<SluzbeniListModel> CreateSluzbeniList([FromBody] SluzbeniListModel sluzbeniList)
        {
            try
            {
                SluzbeniListConfirmation confirmation = sluzbeniListRepository.CreateSluzbeniList(sluzbeniList);
                string location = link.GetPathByAction("GetSluzbeniListovi", "SluzbeniList", new { sluzbeniListId = confirmation.SluzbeniListId });
                return Created(location, confirmation);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while creating an object.");
            }
        }

        [HttpDelete("{sluzbeniListId}")]
        public ActionResult DeleteSluzbeniList(Guid sluzbeniListId)
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
        public ActionResult<SluzbeniListConfirmation> UpdateSluzbeniList(SluzbeniListModel sluzbeniList)
        {
            try
            {
                if (sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId) == null)
                {
                    return NotFound();
                }

                return Ok(sluzbeniListRepository.UpdateSluzbeniList(sluzbeniList));


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }


    }
}
