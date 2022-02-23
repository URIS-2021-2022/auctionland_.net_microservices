using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Koristi se prilikom kreiranja odluke
    /// </summary>
    public class OdlukaoDavanjuuZakupCreationDto
    {
      /// <summary>
      /// Datum donosenja odluke o zakupu
      /// </summary>
        public DateTime Datum_donosenja_odluke { get; set; }
        /// <summary>
        /// Da li je i dalje validna, polje je obavezno
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti validnost odluke")]
        public Boolean validnost { get; set; }
    }
}
