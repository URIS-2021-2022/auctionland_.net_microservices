using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// Model na osnovu kojeg se vrsi autentifikacija
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string KorisnickoIme { get; set; }
        /// <summary>
        /// Lozinka
        /// </summary>
        public string Lozinka { get; set; }
    }
}
