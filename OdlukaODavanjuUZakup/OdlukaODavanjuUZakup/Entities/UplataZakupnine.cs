﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class UplataZakupnine
    {
        [Key]
        public Guid UplataZakupnineID { get; set; } = Guid.NewGuid();
        public string broj_racuna { get; set; }

        public string poziv_na_broj { get; set; }

        public double iznos { get; set; }

        public string svrha_uplate { get; set; }

        public DateTime datum { get; set; }

        public string javno_nadmetanje { get; set; } //entitet
        public string uplatilac { get; set; } //entitet

        public Guid? UgovorOZakupuID { get; set; }
        public UgovoroZakupu ugovorOZakupu { get; set; }
    }
}
