﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Entities
{
    public class KomisijaConfirmation
    {
        public Guid KomisijaId { get; set; } = Guid.NewGuid();
        public PredsednikDto Predsednik { get; set; }
    }
}