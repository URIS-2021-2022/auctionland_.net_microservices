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
        /// <summary>
        /// Prioritet kupca
        /// </summary>
        public Prioritet Prioritet { get; set; }
      
        /// <summary>
        /// Ostvarena površina kupca
        /// </summary>
        public int OstvarenaPovrsina { get; set; }
        
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
        /// Id ovlašćenog lica
        /// </summary>
        public Guid OvlascenoLiceId { get; set; }

        /// <summary>
        /// Id fizičkog lica
        /// </summary>
        public Guid? FizickoLiceId { get; set; }

        /// <summary>
        /// Id pravnog lica
        /// </summary>
        public Guid? PravnoLiceId { get; set; }

        /// <summary>
        /// Lista Id-eva javnih nadmetanja
        /// </summary>
        [NotMapped]
        public List<Guid> JavnaNadmetanja { get; set; } // iz necijeg drugog servisa
    }
}
