using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// DTO za predsednika komisije
    /// </summary>
    public class PredsednikDto
    {
        
      

        /// <summary>
        /// Ime predsednika komisije
        /// </summary>
        public string ImePredsednika { get; set; }
        /// <summary>
        /// Prezime predsednika komisije
        /// </summary>
        public string PrezimePredsednika { get; set; }
        /// <summary>
        /// Email predsednika komisije
        /// </summary>
        public string EmailPredsednika { get; set; }

    }
}