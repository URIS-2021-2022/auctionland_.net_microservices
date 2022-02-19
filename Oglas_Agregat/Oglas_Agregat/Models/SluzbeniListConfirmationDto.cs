using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    /// <summary>
    /// Predstavlja model potvrde službenog lista
    /// </summary>
    public class SluzbeniListConfirmationDto
    {
        /// <summary>
        /// Id službenog lista
        /// </summary>
        public Guid SluzbeniListId { get; set; }

        /// <summary>
        /// Broj lista
        /// </summary>
        public int BrojLista { get; set; }
    }
}
