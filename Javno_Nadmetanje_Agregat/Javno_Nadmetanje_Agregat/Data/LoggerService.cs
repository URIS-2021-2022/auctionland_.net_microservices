using Javno_Nadmetanje_Agregat.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Data
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
                using HttpClient httpClient = new();
                string url = configuration["Services:LoggerService"];
                var log = new LogModel
                {
                    Service = "Javno nadmetanje servis",
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

            catch (Exception ex)
            {
                Console.Write("Error occurred." + ex.Message);
                return false;

            }
        }
    }
}

