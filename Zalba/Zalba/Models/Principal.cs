using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    /// <summary>
    /// Model na osnovu kojeg se vrsi autentifikacija
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisnicko ime
        /// </summary>
        public string KorisnickoIme { get; set; }
        /// <summary>
        /// Lozinka
        /// </summary>
        public string Lozinka { get; set; }
    }
}