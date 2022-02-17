using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    public class LicitacijaUpdateDto
    {
        public Guid LicitacijaId { get; set; }
        public int Broj { get; set; }
        public int Godina { get; set; }
        public DateTime Datum { get; set; }
        public int Ogranicenje { get; set; }
        public int Korak_cene { get; set; }
        public List<string> Lista_dokumentacije_fizicka_lica { get; set; }
        public List<string> Lista_dokumentacije_pravna_lica { get; set; }
        public List<string> JavnoNadmetanje { get; set; } // tudji servis
        public DateTime Rok_za_dostavljanje_prijave { get; set; }
    }
}
