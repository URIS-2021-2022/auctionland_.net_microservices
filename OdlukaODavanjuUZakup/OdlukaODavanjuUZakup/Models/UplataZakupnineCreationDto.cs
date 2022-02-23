using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Koristi se prilikom kreiranja uplate
    /// </summary>
    public class UplataZakupnineCreationDto
    {
        /// <summary>
        /// broj racuna na koji se vrsi uplata
        /// </summary>
        public string broj_racuna { get; set; }
        /// <summary>
        /// Poziv na broj racuna
        /// </summary>
        public string poziv_na_broj { get; set; }
        /// <summary>
        /// iznos uplacene sume
        /// </summary>
        public double iznos { get; set; }
        /// <summary>
        /// Svrha uplate
        /// </summary>
        public string svrha_uplate { get; set; }
        /// <summary>
        /// Datum uplacivanja
        /// </summary>
        public DateTime datum { get; set; }
        /// <summary>
        /// Javno nadmetanje na koje se uplata odnosi, entitet
        /// </summary>
        public string javno_nadmetanje { get; set; } //entitet
        /// <summary>
        /// Uplatilac koji uplacuje uplatu, entitet
        /// </summary>
        public string uplatilac { get; set; } //entitet

        public Guid? UgovorOZakupuID { get; set; }
    }
}
