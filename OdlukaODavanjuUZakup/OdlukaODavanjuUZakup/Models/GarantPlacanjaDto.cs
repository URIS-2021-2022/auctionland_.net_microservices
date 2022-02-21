using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{/// <summary>
/// Model garancije placanja
/// </summary>
    public class GarantPlacanjaDto
    {
        /// <summary>
        /// ID Garanta
        /// </summary>
        public Guid GarantPlacanjaID { get; set; }

        /// <summary>
        /// Opis garanta ima 4 izbora
        /// </summary>
        public GarantEnum Opis_garanta1 { get; set; }

        /// <summary> 
        /// Sekundarni opis garanta
        /// </summary>
        public GarantEnum Opis_garanta2 { get; set; }
    }
}
