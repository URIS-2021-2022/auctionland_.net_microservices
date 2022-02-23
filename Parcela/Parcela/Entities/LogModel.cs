using System;
using Microsoft.Extensions.Logging;

namespace Parcela.Entities
{
    /// <summary>
    /// Klasa LogModel
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// String Service
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// String Level
        /// </summary>
        public LogLevel Level { get; set; }
        /// <summary>
        /// String Method
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// String Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// String Error
        /// </summary>
        public Exception Error { get; set; }
    }
}
