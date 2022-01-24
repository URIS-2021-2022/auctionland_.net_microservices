using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Models
{
    public class PravnoLiceConfirmation
    {
        public Guid PravnoLiceId { get; set; }
        public string Naziv { get; set; }
        public string MaticniBroj { get; set; }
    }
}
