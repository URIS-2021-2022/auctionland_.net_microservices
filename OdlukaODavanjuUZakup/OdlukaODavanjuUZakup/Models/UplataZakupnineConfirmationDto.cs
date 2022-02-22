using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Vraca se prilikom uspesnog kreiranja uplate
    /// </summary>
    public class UplataZakupnineConfirmationDto
    {
        /// <summary>
        /// Id kreirane uplate
        /// </summary>
        public Guid UplataZakupnineID { get; set; }
        /// <summary>
        /// broj racuna na koji je uplata izvrsena
        /// </summary>
        public string broj_racuna { get; set; }
        /// <summary>
        /// datum vrsenja uplate
        /// </summary>
        public DateTime datum { get; set; }
    }
}
