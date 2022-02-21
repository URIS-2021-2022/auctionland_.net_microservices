using System;

namespace Zalba.Models
{
    /// <summary>
    /// Potvrda za tipove zalbi
    /// </summary>
    public class TipZalbeConfirmationDto
    {
        /// <summary>
        /// Id tipa zalbe
        /// </summary>
        public Guid TipZalbeId { get; set; }
        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string TipZalbe { get; set; }
    }
}
