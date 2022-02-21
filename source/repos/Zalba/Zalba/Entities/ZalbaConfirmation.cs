using System;

namespace Zalba.Entities
{
    /// <summary>
    /// Potvrda za zalbe
    /// </summary>
    public class ZalbaConfirmation
    {
        /// <summary>
        /// ID zalbe
        /// </summary>
        public Guid ZalbaId { get; set; }
        /// <summary>
        /// Osnovni podaci o zalbi
        /// </summary>
        public string Zalba { get; set; }
    }
}
