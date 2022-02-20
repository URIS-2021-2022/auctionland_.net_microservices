using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    public class GarantPlacanjaConfirmationDto
    {
        public Guid GarantPlacanjaID { get; set; }
        public GarantEnum Opis_garanta1 { get; set; }
    }
}
