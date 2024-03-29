﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Entities
{
    public class JavnoNadmetanje
    {
        public Guid JavnoNadmetanjeId { get; set; }

        public Guid TipJavnogNadmetanjaId { get; set; }

        [JsonIgnore]
        public TipJavnogNadmetanja TipJavnogNadmetanja { get; set; }

        public Guid StatusJavnogNadmetanjaId { get; set; }

        [JsonIgnore]
        public StatusJavnogNadmetanja StatusJavnogNadmetanja { get; set; }

        public DateTime Datum { get; set; }

        public DateTime VremePocetka { get; set; }

        public DateTime VremeKraja { get; set; }

        public int PocetnaCenaHektar { get; set; }

        public bool Izuzeto { get; set; }

        public int IzlicitiranaCena { get; set; }

        public int PeriodZakupa { get; set; }

        public int BrojUcesnika { get; set; }

        public int VisinaDopuneDepozita { get; set; }

        public int Krug { get; set; }
    }
}
