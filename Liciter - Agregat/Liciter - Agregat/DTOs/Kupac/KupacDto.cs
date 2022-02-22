using Liciter___Agregat.DTOs.FizickoLice;
using Liciter___Agregat.DTOs.OvlascenoLice;
using Liciter___Agregat.DTOs.PravnoLice;
using Liciter___Agregat.Models;
using Liciter___Agregat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Kupac
{
    public class KupacDto
    {
        public PrioritetEnum Prioritet { get; set; }
        public FizickoLiceDto FizickoLice { get; set; }
        public PravnoLiceDto PravnoLice { get; set; }
        public int OstvarenaPovrsina { get; set; }
        public List<OvlascenoLiceDto> OvlascenaLica { get; set; }
        public bool ImaZabranu { get; set; }
        public DateTime DatumPocetkaZabrane { get; set; }
        public int DuzinaTrajanjaZabraneGod { get; set; }
        public DateTime DatumPrestankaZabrane { get; set; }
        public List<Guid> JavnaNadmetanja { get; set; } // iz necijeg drugog servisa
    }
}
