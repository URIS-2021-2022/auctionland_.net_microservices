using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    /// <summary>
    /// Predstavlja model potvrde oglasa
    /// </summary>
    public class OglasConfirmationDto
    {
        /// <summary>
        /// Id oglasa
        /// </summary>
        public Guid OglasId { get; set; }

        /// <summary>
        /// Opis oglasa
        /// </summary>
        public string OpisOglasa { get; set; }


    }
}
