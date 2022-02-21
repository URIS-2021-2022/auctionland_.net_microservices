using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    public class UplataZakupnineDto
    {
        public Guid UplataZakupnineID { get; set; } 
        public string broj_racuna { get; set; }

        public string poziv_na_broj { get; set; }

        public double iznos { get; set; }

        public string svrha_uplate { get; set; }

        public DateTime datum { get; set; }

        public string javno_nadmetanje { get; set; } //entitet
        public string uplatilac { get; set; } //entitet
    }
}
