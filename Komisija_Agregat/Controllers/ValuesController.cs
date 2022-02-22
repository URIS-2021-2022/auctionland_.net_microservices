using Komisija_Agregat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Komisija_Agregat.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IValuesRepository valuesRepository;

        public ValuesController(IValuesRepository valuesRepository)
        {
            this.valuesRepository = valuesRepository;

        }

        [HttpGet]
        public ActionResult<List<string>> GetValues()
        {
            var values = valuesRepository.GetValue();
            if (values == null || values.Count == 0)
            {
                return NoContent();
            }
            return values;
        }


    }

    
}
