﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    public class PredsednikConfirmationDto
    {
        public Guid PredsednikId { get; set; }
        public string ImePredsednika { get; set; }
        public string PrezimePredsednika { get; set; }
        public string EmailPredsednika { get; set; }
    }
}