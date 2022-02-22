using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// DTO za potvrdu predsednika komisije
    /// </summary>
    public class PredsednikConfirmationDto
    {
        public Guid PredsednikId { get; set; }
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