using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    /// <summary>
    /// DTO za potvrdu kreiranja licitacije
    /// </summary>
    public class LicitacijaConfirmationDto
    {
        /// <summary>
        /// Broj licitacije
        /// </summary>
        public int Broj { get; set; }
        /// <summary>
        /// Datum licitacije
        /// </summary>
        public DateTime Datum { get; set; }
    }
}
