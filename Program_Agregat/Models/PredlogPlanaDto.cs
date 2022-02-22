using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Models
{
    /// <summary>
    /// DTO za predlog plana
    /// </summary>
    public class PredlogPlanaDto
    {
        //public Guid PredlogPlanaId { get; set; }
        /// <summary>
        /// Broj dokumenta
        /// </summary>
        public string BrojDokumenta { get; set; }
        /// <summary>
        /// Iskazano misljenje komisije o predlogu plana
        /// </summary>
        public string MisljenjeKomisije { get; set; }
        /// <summary>
        /// Podatak da li je predlog plana usvojen ili ne
        /// </summary>
        public bool Usvojen { get; set; }
        /// <summary>
        /// Datum objavljivanja dokumenta
        /// </summary>
        public DateTime DatumDokumenta { get; set; }

        public ProgramDto ProgramPlana { get; set; }

    }
}