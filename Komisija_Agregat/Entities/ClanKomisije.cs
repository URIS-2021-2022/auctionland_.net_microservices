﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Entities
{
    public class ClanKomisije
    {
        public Guid ClanId { get; set; }
        public string ImeClana { get; set; }
        public string PrezimeClana { get; set; }
        public string EmailClana { get; set; }
    }
}

