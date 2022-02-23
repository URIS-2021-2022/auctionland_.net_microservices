using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Komisija_Agregat.Entities
{
    public class Korisnik
    {
        public Guid KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Salt { get; set; }
        public Guid? TipKorisnikaId { get; set; }
    }
}
