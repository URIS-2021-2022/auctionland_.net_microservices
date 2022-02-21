using System;

namespace Parcela.Models
{
    /// <summary>
    /// Potvrda za parcele parceli
    /// </summary>
    public class ParcelaConfirmationDto
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelaId { get; set; }
        /// <summary>
        /// Osnovni podaci o parceli
        /// </summary>
        public string Parcela { get; set; }
    }
}
