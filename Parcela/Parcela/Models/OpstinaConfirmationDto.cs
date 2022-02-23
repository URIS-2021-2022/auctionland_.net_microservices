using System;

namespace Parcela.Models
{
    /// <summary>
    /// Potvrda za opstine
    /// </summary>
    public class OpstinaConfirmationDto
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

