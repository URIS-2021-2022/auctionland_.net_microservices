using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Models
{
    /// <summary>
    /// Predstavlja model potvrde javnog nadmetanja
    /// </summary>
    public class JavnoNadmetanjeConfirmationDto
    {
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeId { get; set; }

        /// <summary>
        /// Datum javnog nadmetanja
        /// </summary>
        public DateTime Datum { get; set; }
    }
}
