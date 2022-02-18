using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Entities
{
    public class Oglas
    {
        public Guid OglasId { get; set; }
        public DateTime DatumObjave { get; set; }
        public string OpisOglasa { get; set; }
    }
}
