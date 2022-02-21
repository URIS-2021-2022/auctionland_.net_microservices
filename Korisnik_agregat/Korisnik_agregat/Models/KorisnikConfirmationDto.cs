using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Models
{
    /// <summary>
    /// Predstavlja model potvrde korisnika
    /// </summary>
    public class KorisnikConfirmationDto
    {
        /// <summary>
        /// Id korisnika
        /// </summary>
        public Guid KorisnikId { get; set; }
        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string Ime { get; set; }
        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string Prezime { get; set; }
    }
}
