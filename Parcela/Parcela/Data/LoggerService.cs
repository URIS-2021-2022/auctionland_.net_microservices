using Parcela.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.Data
{
    /// <summary>
    /// Klasa LoggerService
    /// </summary>
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// Konfiguracija
        /// </summary>
        public readonly IConfiguration configuration;

        /// <summary>
        /// Konfiguracija
        /// </summary>
        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Metoda log
        /// </summary>
        public async Task<bool> Log(LogLevel level, string method, string message, Exception error = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = configuration["Services:LoggerService"];
                    var log = new LogModel
                    {
                        Service = "Liciter servis",
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
                Console.WriteLine(greska);
                return false;
            }
        }
    }
}