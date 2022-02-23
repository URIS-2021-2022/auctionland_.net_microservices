using Parcela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.ServiceCalls
{
    public class PravnoLiceRepository : IPravnoLiceRepository
    {
        private readonly IConfiguration configuration;

        public PravnoLiceRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<PravnoLiceDto> GetPravnoLiceByIdAsync(Guid PravnoLiceId, HttpRequest httpRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:PravnoLiceService"] }/pravnoLice/{PravnoLiceId}");

                // string token = AuthHelper.GetToken(httpRequest);

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();

                var ol = JsonConvert.DeserializeObject<PravnoLiceDto>(responseContent);

                return ol;
            }
        }
    }
}