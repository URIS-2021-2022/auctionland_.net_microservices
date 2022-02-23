using System;

namespace Parcela.Entities
{
    /// <summary>
    /// Potvrda za delove parcele
    /// </summary>
    public class DeoParceleConfirmation
    {
        /// <summary>
        /// ID dela parcele
        /// </summary>
        public Guid DeoParceleId { get; set; }
        /// <summary>
        /// Osnovni podaci o delu parcele
        /// </summary>
        public string DeoParcele { get; set; }
    }
}
