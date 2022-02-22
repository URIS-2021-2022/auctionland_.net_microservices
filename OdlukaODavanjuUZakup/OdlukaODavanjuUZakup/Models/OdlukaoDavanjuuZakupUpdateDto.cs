using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Koristi se prilikom azuriranja odluke o davanju u zakup
    /// </summary>
    public class OdlukaoDavanjuuZakupUpdateDto
    {
        /// <summary>
        /// ID Odluke
        /// </summary>
          public Guid OdlukaoDavanjuuZakupID { get; set; }
        /// <summary>
        /// Datum donosenja
        /// </summary>
        public DateTime datum_donosenja_odluke { get; set; }
        /// <summary>
        /// Da li je validna, mora biti popunjeno
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti validnost odluke")]
        public Boolean validnost { get; set; }
    }
}
