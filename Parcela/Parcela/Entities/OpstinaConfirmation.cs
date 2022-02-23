using System;

namespace Parcela.Entities
{
    /// <summary>
    /// Potvrda za opstine
    /// </summary>
    public class OpstinaConfirmation
    {
        /// <summary>
        /// Id opstine
        /// </summary>
        public Guid OpstinaId { get; set; }
        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NazivOpstine { get; set; }
    }
}
