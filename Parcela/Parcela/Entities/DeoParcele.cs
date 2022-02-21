using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcela.Entities
{
    /// <summary>
    /// Entitet deo parcele
    /// </summary>
    public class DeoParcele
    {
        /// <summary>
        /// Id dela parcele
        /// </summary>
        public Guid DeoParceleId { get; set; }
        /// <summary>
        /// Id parcele
        /// </summary>
        [ForeignKey("FK_Parcela")]
        public Guid ParcelaId { get; set; }
        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public string PovrsinaDelaParcele { get; set; }
        /// <summary>
        /// Redni broj parcele
        /// </summary>
        public int RbrDelaParcele { get; set; }
    }
}
