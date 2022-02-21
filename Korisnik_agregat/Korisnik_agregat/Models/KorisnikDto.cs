using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Models
{
    /// <summary>
    /// Predstavlja model korisnika
    /// </summary>
    public class KorisnikDto
    {
        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string Ime { get; set; }
        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string Prezime { get; set; }
        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string KorisnickoIme { get; set; }
        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        public string Lozinka { get; set; }
        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// Id tipa korisnika
        /// </summary>
        public Guid? TipKorisnikaId { get; set; }
    }
}
