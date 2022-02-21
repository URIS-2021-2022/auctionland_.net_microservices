using System;

namespace Zalba.Entities
{
    /// <summary>
    /// Potvrda za tipove zalbi
    /// </summary>
    public class TipZalbeConfirmation
    {
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid TipZalbeId { get; set; }
        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string TipZalbe { get; set; }
    }
}
