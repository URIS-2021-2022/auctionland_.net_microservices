﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Licitacija_agregat.Entities
{
    public class Etapa
    {
        public Guid EtapaId { get; set; }
        public DateTime Dan { get; set; }
        public int BrojEtape { get; set; }
        public Guid? LicitacijaId { get; set; }
        [JsonIgnore]
        public Licitacija Licitacija { get; set; }
    }
}
