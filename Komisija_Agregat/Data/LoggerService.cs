﻿using Komisija_Agregat.Models;
using Komisija_Agregat.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Komisija_Agregat.Data
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

            catch 
            {
                return false;
            }
        }
    }
}