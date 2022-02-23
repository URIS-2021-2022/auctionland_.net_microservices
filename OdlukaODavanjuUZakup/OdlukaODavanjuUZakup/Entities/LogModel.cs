using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
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
