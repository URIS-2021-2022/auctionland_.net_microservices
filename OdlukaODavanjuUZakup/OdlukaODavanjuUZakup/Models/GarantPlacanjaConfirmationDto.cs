using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Vraca se kao odgovor da je kreiran
    /// </summary>
    public class GarantPlacanjaConfirmationDto
    {
        /// <summary>
        /// ID garanta placanja
        /// </summary>
        public Guid GarantPlacanjaID { get; set; }
        /// <summary>
        /// Jemstvo, bankarska garancija, garancija nekretninom, zirantska, uplata gotovinom
        /// </summary>
        public string Opis_garanta1 { get; set; }
    }
}
