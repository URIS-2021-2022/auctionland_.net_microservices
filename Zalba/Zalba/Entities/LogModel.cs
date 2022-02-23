using System;
using Microsoft.Extensions.Logging;

namespace Zalba.Entities
{
    public class LogModel
    {
        public string Service { get; set; }
        public LogLevel Level { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
    }
}
