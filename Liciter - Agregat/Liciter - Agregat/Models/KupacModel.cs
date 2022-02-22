using Liciter___Agregat.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Models
{
    public class KupacModel
    {
        [Key]
        public Guid KupacId { get; set; }

        [Required]
        public PrioritetEnum Prioritet { get; set; }
        public int OstvarenaPovrsina { get; set; }
        public bool ImaZabranu { get; set; }
        public DateTime DatumPocetkaZabrane { get; set; }
        public int DuzinaTrajanjaZabraneGod { get; set; }
        public DateTime DatumPrestankaZabrane { get; set; }

        public FizickoLiceModel FizickoLice { get; set; }
        public Guid? FizickoLiceId { get; set; }
        public Guid? PravnoliceId { get; set; }
        public PravnoLiceModel PravnoLice { get; set; }

     
        public List<OvlascenoLiceModel> OvlascenaLica { get; set; }

        public Guid? JavnoNadmetanjeId { get; set; }

        [NotMapped]
        public List<Guid> JavnaNadmetanjaId { get; set; } // iz necijeg drugog servisa


        // public List<Uplata> Uplate { get; set; } // uplate su entitet

    }
}
