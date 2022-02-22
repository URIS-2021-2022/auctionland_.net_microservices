using Liciter___Agregat.Models;
using Liciter___Agregat.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Kupac
{
    public class KupacCreationDto
    {
        public PrioritetEnum Prioritet { get; set; }
      
        public int OstvarenaPovrsina { get; set; }
        
        public bool ImaZabranu { get; set; }
        public DateTime DatumPocetkaZabrane { get; set; }
        public int DuzinaTrajanjaZabraneGod { get; set; }
        public DateTime DatumPrestankaZabrane { get; set; }


        public Guid OvlascenoLiceId { get; set; }
        public Guid? FizickoLiceId { get; set; }
        public Guid? PravnoLiceId { get; set; }

        [NotMapped]
        public List<Guid> JavnaNadmetanja { get; set; } // iz necijeg drugog servisa
    }
}
