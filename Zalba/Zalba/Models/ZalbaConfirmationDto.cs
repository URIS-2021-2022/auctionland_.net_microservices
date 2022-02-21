using System;

namespace Zalba.Models
{
    /// <summary>
    /// Potvrda za zalbe zalbi
    /// </summary>
    public class ZalbaConfirmationDto
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
