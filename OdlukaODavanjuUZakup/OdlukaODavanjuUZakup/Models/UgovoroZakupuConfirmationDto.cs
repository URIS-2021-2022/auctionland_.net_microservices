using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Vraca se prilikom uspesnog kreiranja ugovor
    /// </summary>
    public class UgovoroZakupuConfirmationDto
    {
        /// <summary>
        /// ID ugovora o zakupu
        /// </summary>
        public Guid UgovoroZakupuID { get; set; }
        /// <summary>
        /// Broj ugovora pod kojim se zavodi
        /// </summary>
        public string zavodni_Broj { get; set; }
        /// <summary>
        /// Datum kada je ugovor potpisan
        /// </summary>
        public DateTime datum_potpisa { get; set; }
    }
}
