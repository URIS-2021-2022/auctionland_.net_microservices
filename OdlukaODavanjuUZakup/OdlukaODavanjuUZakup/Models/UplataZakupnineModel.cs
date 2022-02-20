using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    public class UplataZakupnineModel
    {
        public Guid UplataZakupnineID { get; set; }
        public string broj_racuna { get; set; }

        public string poziv_na_broj { get; set; }

        public double iznos { get; set; }

      

        public string svrha_uplate { get; set; }

        public DateTime datum { get; set; }

       // public Javno_Nadmetanje javno_nadmetanje { get; set; }
      //  public Kupac uplatilac { get; set; }
    }
}
