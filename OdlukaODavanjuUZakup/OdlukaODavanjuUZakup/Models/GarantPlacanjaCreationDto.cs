using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Koristi se prilikom kreiranja
    /// </summary>
    public class GarantPlacanjaCreationDto
    {
        /// <summary>
        /// Jemstvo, bankarska garancija, garancija nekretninom, zirantska, uplata gotovinom
        /// </summary>
        public string Opis_garanta1 { get; set; }
        /// <summary>
        /// Jemstvo, bankarska garancija, garancija nekretninom, zirantska, uplata gotovinom
        /// </summary>
        public string Opis_garanta2 { get; set; }

        public Guid? UgovorOZakupuID { get; set; }
    }
}
