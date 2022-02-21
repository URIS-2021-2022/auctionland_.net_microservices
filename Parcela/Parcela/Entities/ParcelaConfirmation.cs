using System;

namespace Parcela.Entities
{
    /// <summary>
    /// Potvrda za parcele
    /// </summary>
    public class ParcelaConfirmation
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
