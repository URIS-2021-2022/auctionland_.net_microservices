using System;

namespace Zalba.Models
{
    /// <summary>
    /// DTO za zalbe
    /// </summary>
    public class ZalbaDto
    {
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid TipId { get; set; }
        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        public DateTime DatumPodnosenjaZalbe { get; set; }
        /// <summary>
        /// ID podnosioca zalbe
        /// </summary>
        public Guid? PodnosilacZalbe { get; set; }
        /// <summary>
        /// Razlog zalbe zalbe
        /// </summary>
        public string RazlogZalbe { get; set; }
        /// <summary>
        /// Obrazlozenje zalbe
        /// </summary>
        public string Obrazlozenje { get; set; }
        /// <summary>
        /// Datum resenja zalbe
        /// </summary>
        public DateTime DatumResenja { get; set; }
        /// <summary>
        /// Broj resenja zalbe
        /// </summary>
        public string BrojResenja { get; set; }
        /// <summary>
        /// Status zalbe
        /// </summary>
        public string StatusZalbe { get; set; }
        /// <summary>
        /// Broj odluke zalbe
        /// </summary>
        public string BrojOdluke { get; set; }
        /// <summary>
        /// Radnja na osnovu zalbe
        /// </summary>
        public string RadnjaNaOsnovuZalbe { get; set; }
    }
}
