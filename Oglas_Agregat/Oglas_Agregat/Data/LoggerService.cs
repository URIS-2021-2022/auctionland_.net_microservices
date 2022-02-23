using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public class LoggerService : ILoggerService
    {
        public readonly IConfiguration configuration;

        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> Log(LogLevel level, string method, string message, Exception error = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = configuration["Services:LoggerService"];
                    var log = new LogModel
                    {
                        Service = "Oglas servis",
                        Level = level,
                        Message = message,
                        Error = error,
                        Method = method
                    };

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage response = httpClient.PostAsync(url, content).Result;



                    return await Task.FromResult(response.IsSuccessStatusCode);

                }
            }

            catch (Exception ex)
            {
                string greska = ex.Message;
                return false;
            }
        }
    }
}
