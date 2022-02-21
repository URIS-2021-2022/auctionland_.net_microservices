using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcela.Entities
{
    /// <summary>
    /// Entitet opstina
    /// </summary>
    public class Opstina
    {
        /// <summary>
        /// Id opstine
        /// </summary>
        public Guid OpstinaId { get; set; }
        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NazivOpstine { get; set; }
        /// <summary>
        /// Lista parcela
        /// </summary>
        public List<ParcelaM> Parcele { get; set; }

    }
}
