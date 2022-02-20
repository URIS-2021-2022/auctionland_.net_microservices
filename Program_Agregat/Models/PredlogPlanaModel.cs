using System;

namespace Program_Agregat.Models
{
    public class PredlogPlanaModel
    {
        public Guid PredlogPlanaId { get; set; }
        public int BrojDokumenta { get; set; }
        public string MisljenjeKomisije { get; set; }
        public bool Usvojen { get; set; }
        public date DatumDokumenta { get; set; }

    }
}