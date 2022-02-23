using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Entities
{
    public class Licitacija
    {
        public Guid LicitacijaId { get; set; }
        public int Broj { get; set; }
        public int Godina { get; set; }
        public DateTime Datum { get; set; }
        public int Ogranicenje { get; set; }
        public int Korak_cene { get; set; }
        [NotMapped]
        public List<string> Lista_dokumentacije_fizicka_lica { get; set; }
        [NotMapped]
        public List<string> Lista_dokumentacije_pravna_lica { get; set; }
        [NotMapped]
        public List<string> JavnoNadmetanje { get; set; } 
        public DateTime Rok_za_dostavljanje_prijave { get; set; }
        public List<Etapa> ListaEtapa { get; set; }

    }
}
