using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public interface ILoggerService
    {
        public bool Log(LogLevel level, string method, string message, Exception error = null) ;
    }
}
