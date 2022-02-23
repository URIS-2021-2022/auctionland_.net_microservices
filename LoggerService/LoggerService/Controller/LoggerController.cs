using LoggerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        public readonly ILoggerManager loggerManager;

        public LoggerController(ILoggerManager loggerManager)
        {
            this.loggerManager = loggerManager;
        }

        [HttpPost]
        public void Post(LogModel log)
        {
            if (log.Level == LogLevel.Information)
                loggerManager.LogInfo(log.Message);

            if (log.Level == LogLevel.Error)
                loggerManager.LogError(log.Message);

            if (log.Level == LogLevel.Warning)
                loggerManager.LogWarn(log.Message);

            if (log.Level == LogLevel.Debug)
                loggerManager.LogDebug(log.Message);

        }

    }
}

