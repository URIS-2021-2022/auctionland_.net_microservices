using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Models
{
    /// <summary>
    /// Predstavlja model kreiranja korisnika
    /// </summary>
    public class KorisnikCreationDto
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
        [Required]
        public string KorisnickoIme { get; set; }
        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        [Required]
        public string Lozinka { get; set; }
        /// <summary>
        /// Id tipa korisnika
        /// </summary>
        public Guid? TipKorisnikaId { get; set; }
    }
}
