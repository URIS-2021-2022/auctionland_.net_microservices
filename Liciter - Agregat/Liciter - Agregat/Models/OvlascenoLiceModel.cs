﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Models
{
    public class OvlascenoLiceModel
    {
        [Key]
        public Guid OvlascenoLiceId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string JMBG_Br_Pasosa { get; set; }
        public string Adresa { get; set; } 
        public KupacModel Kupac { get; set; }
        public Guid KupacId { get; set; }
        public string Drzava { get; set; }
        public int BrojTable { get; set; }
    }
}
