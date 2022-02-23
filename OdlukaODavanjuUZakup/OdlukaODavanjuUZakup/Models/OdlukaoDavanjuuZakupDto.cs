using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Model Odluke o davanju u zakup
    /// </summary>
    public class OdlukaoDavanjuuZakupDto
    {
        /// <summary>
        /// Id odluke o davanju u zakup
        /// </summary>
        public Guid OdlukaoDavanjuuZakupID { get; set; } 
        /// <summary>
        /// datum donosenja odluke
        /// </summary>
        public DateTime datum_donosenja_odluke { get; set; }
        /// <summary>
        /// Da li je odluka validna
        /// </summary>
        public Boolean validnost { get; set; }

    }
}
