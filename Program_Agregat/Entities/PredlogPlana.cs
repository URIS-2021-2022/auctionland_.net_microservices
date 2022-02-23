using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;

namespace Program_Agregat.Entities
{
    public class PredlogPlana
    {
        [Key]
        public Guid PredlogPlanaId { get; set; }
        public string BrojDokumenta { get; set; }
        public string MisljenjeKomisije { get; set; }
        public bool Usvojen { get; set; }
        public DateTime DatumDokumenta { get; set; }
        public ProgramEntity ProgramPlana { get; set; }
        public Guid? ProgramId { get; set; }

    }
}