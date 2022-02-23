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
        //private readonly IAuthHelper authHelper;


        public JavnoNadmetanjeService(IConfiguration configuration)//, IAuthHelper authHelper)
        {
            this.Configuration = configuration;
            //this.authHelper = authHelper;
        }

        public async Task<JavnoNadmetanjeDto> GetJavnoNadmetanjeByIdAsync(Guid javnoNadmetanjeId, HttpRequest httpRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ Configuration["Services:JavnoNadmetanjeService"] }/javno-nadmetanje/{javnoNadmetanjeId}"); //ovde da li je dobro

                //string token = AuthHelper.GetToken(httpRequest);

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.Timeout = TimeSpan.FromMinutes(1000);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();

                var ol = JsonConvert.DeserializeObject<JavnoNadmetanjeDto>(responseContent);

                return ol;
            }
        }
    }
    
}
