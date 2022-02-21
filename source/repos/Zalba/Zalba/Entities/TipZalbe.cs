using System;
using System.Collections.Generic;

namespace Zalba.Entities
{
    /// <summary>
    /// Entitet tip zalbe
    /// </summary>
    public class TipZalbe
    {
        /// <summary>
        /// Id tipa zalbe
        /// </summary>
        public Guid TipZalbeId { get; set; }
        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string NazivTipa { get; set; }
        /// <summary>
        /// Lista zalbi
        /// </summary>
        public List<ZalbaM> Zalbe { get; set; }
    }
}
