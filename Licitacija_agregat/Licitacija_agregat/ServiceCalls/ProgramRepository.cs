using Licitacija_agregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Licitacija_agregat.ServiceCalls
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly IConfiguration configuration;

        public ProgramRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ProgramDto> GetProgramByIdAsync(Guid ProgramId, HttpRequest httpRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:ProgramService"] }/Program/{ProgramId}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();

                var ol = JsonConvert.DeserializeObject<ProgramDto>(responseContent);

                return ol;
            }
        }
    }
}
