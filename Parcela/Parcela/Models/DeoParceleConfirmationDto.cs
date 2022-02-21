using System;

namespace Parcela.Models
{
    /// <summary>
    /// Potvrda za delove parcele
    /// </summary>
    public class DeoParceleConfirmationDto
    {
        /// <summary>
        /// Id dela parcele
        /// </summary>
        public Guid DeoParceleId { get; set; }
        /// <summary>
        /// Id parcele
        /// </summary>
        public Guid ParcelaId { get; set; }
        /// <summary>
        /// Naziv dela parcele
        /// </summary>
        public string PovrsinaDelaParcele { get; set; }
        /// <summary>
        /// Lista parceli
        /// </summary>
        public int RbrDelaParcele { get; set; }
    }
}
