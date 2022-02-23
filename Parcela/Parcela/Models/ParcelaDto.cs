using System;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za parcele
    /// </summary>
    public class ParcelaDto
    {
        /// <summary>
        /// ID dela parcele
        /// </summary>
        public Guid TipId { get; set; }
        /// <summary>
        /// Datum podnosenja parcele
        /// </summary>
        public DateTime DatumPodnosenjaParcele { get; set; }
        /*/// <summary>
        /// ID podnosioca parcele
        /// </summary>
        public Guid PodnosilacParcele { get; set; }*/
        /// <summary>
        /// Razlog parcele parcele
        /// </summary>
        public string RazlogParcele { get; set; }
        /// <summary>
        /// Obrazlozenje parcele
        /// </summary>
        public string Obrazlozenje { get; set; }
        /// <summary>
        /// Datum resenja parcele
        /// </summary>
        public DateTime DatumResenja { get; set; }
        /// <summary>
        /// Broj resenja parcele
        /// </summary>
        public string BrojResenja { get; set; }
        /// <summary>
        /// Status parcele
        /// </summary>
        public string StatusParcele { get; set; }
        /// <summary>
        /// Broj odluke parcele
        /// </summary>
        public string BrojOdluke { get; set; }
        /// <summary>
        /// Radnja na osnovu parcele
        /// </summary>
        public string RadnjaNaOsnovuParcele { get; set; }
    }
}
