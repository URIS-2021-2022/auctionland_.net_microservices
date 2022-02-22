using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Entities
{
    public class PredsednikConfirmation
    {
        
        public Guid PredsednikId { get; set; } = Guid.NewGuid();
        public string ImePredsednika { get; set; }
        public string PrezimePredsednika { get; set; }
        public string EmailPredsednika { get; set; }
    }
}
