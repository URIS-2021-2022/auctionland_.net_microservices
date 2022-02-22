using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Parcela.Data
{
    public interface ILoggerService
    {
        public Task<bool> Log(LogLevel level, string method, string message, Exception error = null);
    }
}
