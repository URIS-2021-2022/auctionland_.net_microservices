using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// DTO za potvrdu komisije
    /// </summary>
    public class KomisijaConfirmationDto
    {
        
        public Guid KomisijaId { get; set; }
        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public string Predsednik { get; set; }
    }
}
