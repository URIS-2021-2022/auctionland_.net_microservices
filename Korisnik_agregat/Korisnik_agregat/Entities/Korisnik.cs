using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Entities
{
    public class Korisnik
    {
        public Guid KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Salt { get; set; }
        public Guid? TipKorisnikaID { get; set; }
    }
}
