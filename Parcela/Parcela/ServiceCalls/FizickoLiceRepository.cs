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
    public class FizickoLiceRepository : IFizickoLiceRepository
    {
        private readonly IConfiguration configuration;

        public FizickoLiceRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<FizickoLiceDto> GetFizickoLiceByIdAsync(Guid FizickoLiceId, HttpRequest httpRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:FizickoLiceService"] }/fizickoLice/{FizickoLiceId}");

                // string token = AuthHelper.GetToken(httpRequest);

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();

                var ol = JsonConvert.DeserializeObject<FizickoLiceDto>(responseContent);

                return ol;
            }
        }
    }
}