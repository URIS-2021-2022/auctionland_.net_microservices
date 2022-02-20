using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class GarantPlacanja
    {
        public Guid GarantPlacanjaID { get; set; }
        public GarantEnum Opis_garanta1 { get; set; }
        public GarantEnum Opis_garanta2 { get; set; }
    }
}
