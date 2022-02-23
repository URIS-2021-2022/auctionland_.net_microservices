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
        /// <summary>
        /// Prioritet kupca
        /// </summary>
        public PrioritetEnum Prioritet { get; set; }

        /// <summary>
        /// Fizičko lice DTO
        /// </summary>
        public FizickoLiceDto FizickoLice { get; set; }

        /// <summary>
        /// Pravno lice DTO
        /// </summary>
        public PravnoLiceDto PravnoLice { get; set; }

        /// <summary>
        /// Ostvarena površina kupca
        /// </summary>
        public int OstvarenaPovrsina { get; set; }

        /// <summary>
        /// Lista Ovlašćenih lica
        /// </summary>
        public List<OvlascenoLiceDto> OvlascenaLica { get; set; }

        /// <summary>
        /// Obeležje koje govori da li kupac ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// Datum početka zabrane
        /// </summary>
        public DateTime DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// Dužina trajanje zabrane u godinama
        /// </summary>
        public int DuzinaTrajanjaZabraneGod { get; set; }

        /// <summary>
        /// Datum prestanka zabrane
        /// </summary>
        public DateTime DatumPrestankaZabrane { get; set; }

        /// <summary>
        /// Lista Id-eva javnih nadmetanja
        /// </summary>
        public List<Guid> JavnaNadmetanja { get; set; } // iz necijeg drugog servisa
    }
}
