using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public interface ILoggerService
    {
        public Task<bool> Log(LogLevel level, string method, string message, Exception error = null);
    }
}
