using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public interface ILoggerService
    {
        public Task<bool> Log(LogLevel level, string method, string message, Exception error = null);
    }
}
