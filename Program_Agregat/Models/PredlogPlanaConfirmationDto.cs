using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Models
{
    public class PredlogPlanaConfirmationDto
    {
        public Guid PredlogPlanaId { get; set; }
        public string BrojDokumenta { get; set; }
        public string MisljenjeKomisije { get; set; }
        public bool Usvojen { get; set; }
        public string DatumDokumenta { get; set; }
    }
}