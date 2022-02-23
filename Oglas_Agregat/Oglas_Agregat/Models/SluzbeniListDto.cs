using Oglas_Agregat.Entities;
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

        /// <summary>
        /// Lista oglasa koji su objavljeni u službenom listu
        /// </summary>
        public List<Oglas> ListaOglasa { get; set; }
    }
}
