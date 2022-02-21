using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Models
{
    /// <summary>
    /// Predstavlja model potvrde tipa javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanjaConfirmationDto
    {
        /// <summary>
        /// Id tipa javnog nadmetanja
        /// </summary>
        public Guid TipJavnogNadmetanjaId { get; set; }

        /// <summary>
        /// Naziv tipa javnog nadmetanja
        /// </summary>
        public string NazivTipaJavnogNadmetanja { get; set; }
    }
}
