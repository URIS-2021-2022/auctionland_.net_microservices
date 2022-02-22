using Korisnik_agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Models
{
    /// <summary>
    /// Prestavlja model tipa korisnika
    /// </summary>
    public class TipKorisnikaDto
    {
        /// <summary>
        /// Id tipa korisnika
        /// </summary>
        public Guid TipKorisnikaId { get; set; }
        /// <summary>
        ///  Naziv tipa korisnika
        /// </summary>
        public string NazivTipa { get; set; }
        /// <summary>
        /// Lista korisnika datog tipa korisnika
        /// </summary>
        public List<Korisnik> ListaKorisnika { get; set; }
    }
}
