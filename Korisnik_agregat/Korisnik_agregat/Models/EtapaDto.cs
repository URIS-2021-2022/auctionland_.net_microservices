using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Models
{
    public class EtapaDto
    {
        /// <summary>
        /// Dan etape
        /// </summary>
        public DateTime Dan { get; set; }
        /// <summary>
        /// Broj etape
        /// </summary>
        public int BrojEtape { get; set; }
        public Guid? LicitacijaId { get; set; }
    }
}
