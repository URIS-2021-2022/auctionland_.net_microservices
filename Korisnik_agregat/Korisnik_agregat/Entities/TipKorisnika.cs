using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Korisnik_agregat.Entities
{
    public class TipKorisnika
    {
        public Guid TipKorisnikaId { get; set; }
        public string NazivTipa { get; set; }
        public List<Korisnik> ListaKorisnika { get; set; }
    }
}
