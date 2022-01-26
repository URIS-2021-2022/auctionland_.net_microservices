using Liciter___Agregat.Models;
using Liciter___Agregat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Kupac
{
    public class KupacUpdateDto
    {
        public Guid KupacId { get; set; }
        public PrioritetEnum Prioritet { get; set; }
        public FizickoLiceModel FizickoLice { get; set; }
        public PravnoLiceModel PravnoLice { get; set; }
        public int OstvarenaPovrsina { get; set; }
        public List<string> Uplate { get; set; } // uplate su entitet
        public OvlascenoLiceModel OvlascenoLice { get; set; }
        public bool ImaZabranu { get; set; }
        public DateTime DatumPocetkaZabrane { get; set; }
        public int DuzinaTrajanjaZabraneGod { get; set; }
        public DateTime DatumPrestankaZabrane { get; set; }
        public List<string> JavnaNadmetanja { get; set; } // iz necijeg drugog servisa
    }
}
