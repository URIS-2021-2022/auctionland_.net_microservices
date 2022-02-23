using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Oglas_Agregat.ServiceCalls
{
    public class JavnoNadmetanjeService : IJavnoNadmetanjeService
    {
        private readonly IConfiguration Configuration;


        public JavnoNadmetanjeService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public async Task<JavnoNadmetanjeDto> GetJavnoNadmetanjeByIdAsync(Guid javnoNadmetanjeId, HttpRequest httpRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ Configuration["Services:JavnoNadmetanjeService"] }/javno-nadmetanje/{javnoNadmetanjeId}"); //ovde da li je dobro

                client.Timeout = TimeSpan.FromMinutes(1000);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();

                var ol = JsonConvert.DeserializeObject<JavnoNadmetanjeDto>(responseContent);

                return ol;
            }
        }
    }
    
}
