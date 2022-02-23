using System;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za delove parcele
    /// </summary>
    public class DeoParceleDto
    {
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
