using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Models
{
    /// <summary>
    /// Predstavlja model za modifikaciju korisnika
    /// </summary>
    public class KorisnikUpdateDto
    {
        /// <summary>
        /// Id korisnika
        /// </summary>
        public Guid KorisnikId { get; set; }
        /// <summary>
        /// Id tipa korisnika
        /// </summary>
        public Guid TipKorisnikaId { get; set; }
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
        /// Lozinka
        /// </summary>
        public string Lozinka { get; set; }
        
    }
}
