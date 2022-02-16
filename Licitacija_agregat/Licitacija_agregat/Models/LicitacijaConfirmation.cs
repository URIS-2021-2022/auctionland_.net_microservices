using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    public class LicitacijaConfirmation
    {
        public Guid LicitacijaId { get; set; }
        public int Broj { get; set; }
        public DateTime Datum { get; set; }
    }
}
