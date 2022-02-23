using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Parcela.Data
{
    /// <summary>
    /// Interfejs za LoggerService
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Metoda za LoggerService
        /// </summary>
        public Task<bool> Log(LogLevel level, string method, string message, Exception error = null);
    }
}
