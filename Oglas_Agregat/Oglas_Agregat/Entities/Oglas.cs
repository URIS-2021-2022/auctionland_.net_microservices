using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oglas_Agregat.Entities
{
    public class Oglas
    {
        public Guid OglasId { get; set; }
        public DateTime DatumObjave { get; set; }
        public DateTime RokZaZalbu { get; set; }
        public string OpisOglasa { get; set; }
        public Guid ObjavljenUListuId { get; set; }

        public Guid? JavnoNadmetanjeId { get; set; }


        [JsonIgnore]
        public SluzbeniList ObjavljenUListu { get; set; }
    }
}
