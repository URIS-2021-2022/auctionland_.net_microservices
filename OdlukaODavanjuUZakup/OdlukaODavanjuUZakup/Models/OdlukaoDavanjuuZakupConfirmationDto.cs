using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Vraca se kao odgovor da je odluka kreirana
    /// </summary>
    public class OdlukaoDavanjuuZakupConfirmationDto
    {
        /// <summary>
        /// ID odluke o davanju u zakupu
        /// </summary>
        public Guid OdlukaoDavanjuuZakupID { get; set; }
        /// <summary>
        /// Da li je i dalje validna
        /// </summary>
        public Boolean validnost { get; set; }
    }
}
