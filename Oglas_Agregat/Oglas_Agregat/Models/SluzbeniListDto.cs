using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    /// <summary>
    /// Predstavlja model službenog lista
    /// </summary>
    public class SluzbeniListDto
    {
        //public Guid SluzbeniListId { get; set; }

        /// <summary>
        /// Datum izdavanja službenog lista
        /// </summary>
        public DateTime DatumIzdanja { get; set; }

        /// <summary>
        /// Broj lista
        /// </summary>
        public int BrojLista { get; set; }
    }
}
