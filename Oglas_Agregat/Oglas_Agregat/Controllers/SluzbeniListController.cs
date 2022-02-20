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
    /// <summary>
    /// Predstavlja kontroler službenog lista
    /// </summary>
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

        /// <summary>
        /// Vraća sve službene listove na osnovu prosleđenog filtera
        /// </summary>
        /// <param name="BrojLista">Broj službenog lista</param>
        /// <returns>Listu službenih listova</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<SluzbeniListDto>> GetSluzbeniListovi(int BrojLista)
        {
            var sluzbeniListovi = sluzbeniListRepository.GetSluzbeniListovi(BrojLista);
            if (sluzbeniListovi == null || sluzbeniListovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<SluzbeniListDto>>(sluzbeniListovi));
        }

        /// <summary>
        /// Vraća službeni list po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="sluzbeniListId"></param>
        /// <returns>Objekat službenog lista</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Kreira novi službeni list
        /// </summary>
        /// <param name="sluzbeniList"></param>
        /// <returns>Potvrdu o kreiranom službenom listu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog službenog lista
        /// {
        ///     "BrojLista" : 3,
        ///     "DatumIzdanja" : "2006-05-16T05:50:06"
        /// }
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<SluzbeniListConfirmationDto> CreateSluzbeniList([FromBody] SluzbeniListCreateDto sluzbeniList)
        {
            try
            {
                var sluzbeniListEntity = mapper.Map<SluzbeniList>(sluzbeniList);

                var confirmation = sluzbeniListRepository.CreateSluzbeniList(sluzbeniListEntity);

                sluzbeniListRepository.SaveChanges();

                string location = link.GetPathByAction("GetSluzbeniListovi", "SluzbeniList", new { sluzbeniListId = confirmation.SluzbeniListId });
                return Created(location, mapper.Map<SluzbeniListConfirmationDto>(confirmation));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while creating an object.");
            }
        }

        /// <summary>
        /// Briše službeni list po zadatoj vrednosti id-a
        /// </summary>
        /// <param name="sluzbeniListId"></param>
        /// <returns>Prazan payload</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                sluzbeniListRepository.SaveChanges();
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while deleting the object.");
            }
        }

        /// <summary>
        /// Menja vrednosti postojećeg službenog lista
        /// </summary>
        /// <param name="sluzbeniList"></param>
        /// <returns>Potvrdu o izmenjenom objektu</returns>
        /// <remarks>
        /// Primer zahteva za izmenu postojećeg službenog lista
        /// {
        ///     "SluzbeniListId": "00f78e6b-a2bb-43b5-b3bb-f5708d1a5129",
        ///     "BrojLista" : 3,
        ///     "DatumIzdanja" : "2006-05-16T05:50:06"
        /// }
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<SluzbeniListConfirmationDto> UpdateSluzbeniList(SluzbeniListUpdateDto sluzbeniList)
        {
            try
            {
                var confirmation = sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId);
                if (sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId) == null)
                {
                    return NotFound();
                }

                //
                confirmation.BrojLista = sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId).BrojLista;
                confirmation.DatumIzdanja = sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId).DatumIzdanja;


                sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId).BrojLista = sluzbeniList.BrojLista;
                sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId).DatumIzdanja = sluzbeniList.DatumIzdanja;
                //


                SluzbeniList sluzbeniListEntity = mapper.Map<SluzbeniList>(sluzbeniList);
                sluzbeniListRepository.SaveChanges();                
                return Ok(mapper.Map<SluzbeniListConfirmationDto>(confirmation));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred while updating the object.");
            }
        }

        /// <summary>
        /// Prikazuje sve moguće tipove zahteva 
        /// </summary>
        /// <returns>Listu mogućih zahteva</returns>
        [HttpOptions]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
