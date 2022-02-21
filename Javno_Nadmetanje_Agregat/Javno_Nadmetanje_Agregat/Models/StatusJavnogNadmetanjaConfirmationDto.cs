using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Models
{
    /// <summary>
    /// Predstavlja model potvrde statusa javnog nadmetanja
    /// </summary>
    public class StatusJavnogNadmetanjaConfirmationDto
    {
        /// <summary>
        /// Id statusa javnog nadmetanja
        /// </summary>
        public Guid StatusJavnogNadmetanjaId { get; set; }

        /// <summary>
        /// Naziv statusa javnog nadmetanja
        /// </summary>
        public string NazivStatusaJavnogNadmetanja { get; set; }
    }
}
