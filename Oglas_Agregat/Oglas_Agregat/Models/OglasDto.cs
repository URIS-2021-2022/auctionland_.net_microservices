using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    public class OglasDto
    {
        //obrisala id jer necu da njega prikazuje korisniku 
        public DateTime DatumObjave { get; set; }
        public string OpisOglasa { get; set; }
    }
}
