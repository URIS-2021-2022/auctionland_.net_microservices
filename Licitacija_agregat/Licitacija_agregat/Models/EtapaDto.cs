using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    /// <summary>
    /// Predstavlja model etape
    /// </summary>
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
    }
}
