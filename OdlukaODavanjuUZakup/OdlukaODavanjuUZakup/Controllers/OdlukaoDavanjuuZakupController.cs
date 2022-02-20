using Microsoft.AspNetCore.Mvc;
using OdlukaODavanjuUZakup.Data;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Controllers
{
    [ApiController]
    [Route("api/OdlukaeDavanjuuZakup")]
    class OdlukaoDavanjuuZakupController : ControllerBase
    {
        private readonly IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository;

        public OdlukaoDavanjuuZakupController(IOdlukaoDavanjuuZakupRepository odlukaoDavanjuuZakupRepository)
        {
            this.odlukaoDavanjuuZakupRepository = odlukaoDavanjuuZakupRepository;
        }
        public List<OdlukaoDavanjuuZakupModel> GetOdluke()
        {
            var odluke = odlukaoDavanjuuZakupRepository.GetOdluke();
            return odluke;
        }
    }
}
