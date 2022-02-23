using Liciter___Agregat.DTOs.Kupac;
using Liciter___Agregat.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class JavnaNadmetanjeService : IJavnaNadmetanjaService
    {
        private readonly IConfiguration Configuration;
        private readonly IAuthHelper AuthHelper;

        public JavnaNadmetanjeService(IConfiguration configuration, IAuthHelper authHelper)
        {
            this.Configuration = configuration;
            this.AuthHelper = authHelper;
        }
        public JavnoNadmetanjeDto GetJavnoNadmetanjeById(Guid javnoNadmetanjeId, HttpRequest httpRequest)
        {

            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ Configuration["Services:JavnoNadmetanje"] }/javnoNadmetanje/{javnoNadmetanjeId}");

                string token = AuthHelper.GetToken(httpRequest);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = response.Content.ToString();

                var ol = JsonConvert.DeserializeObject<JavnoNadmetanjeDto>(responseContent);

                return ol;
            }
        }
    }
}
