using Korisnik_agregat.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Korisnik_agregat.ServiceCalls
{
    public class KorisnikovaLicitacija : IKorisnikovaLicitacija
    {
        private readonly IConfiguration configuration;

        public KorisnikovaLicitacija(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetKorisnikoveLicitacije(List<KorisnikovaLicitacijaDto> licitacije)
        {
            throw new NotImplementedException();
        }

        /*public bool GetKorisnikoveLicitacije(List<KorisnikovaLicitacijaDto> licitacije)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:ExamBillingService"];
                Uri url = new Uri($"{ configuration["Services:ExamBillingService"] }api/examBillings");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(bill));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.GetAsync(url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
        }*/
    }
}
