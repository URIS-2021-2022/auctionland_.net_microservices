using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Program_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Program_Agregat.ServiceCalls
{
    public class KomisijaService : IKomisijaService
    {
        private readonly IConfiguration Configuration;
        //private readonly IAuthHelper authHelper;


        public KomisijaService(IConfiguration configuration)//, IAuthHelper authHelper)
        {
            this.Configuration = configuration;
            //this.authHelper = authHelper;
        }

        public async Task<KomisijaDto> GetKomisijaByIdAsync(Guid komisijaId, HttpRequest httpRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ Configuration["Services:Komisija"] }/Komisija/{komisijaId}"); //ovde da li je dobro

                //string token = AuthHelper.GetToken(httpRequest);

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.Timeout = TimeSpan.FromMinutes(1000);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();

                var ol = JsonConvert.DeserializeObject<KomisijaDto>(responseContent);

                return ol;
            }
        }
    }

}
