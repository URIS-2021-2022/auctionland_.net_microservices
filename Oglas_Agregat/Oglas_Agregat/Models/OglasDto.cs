using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    /// <summary>
    /// Predstavlja model oglasa
    /// </summary>
    public class OglasDto
    {
        //obrisala id jer necu da njega prikazuje korisniku 

        /// <summary>
        /// Datum objave oglasa
        /// </summary>
        public DateTime DatumObjave { get; set; }

        /// <summary>
        /// Rok za žalbu na oglas
        /// </summary>
        public DateTime RokZaZalbu { get; set; }

        /// <summary>
        /// Opis oglasa
        /// </summary>
        public string OpisOglasa { get; set; }

        /// <summary>
        /// Id službenog lista u kom je objavljen oglas
        /// </summary>
        public Guid ObjavljenUListuId { get; set; }
    }
}
