using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    public class OdlukaoDavanjuuZakupDto
    {
        public Guid OdlukaoDavanjuuZakupID { get; set; }
        public DateTime datum_donosenja_odluke { get; set; }

        public Boolean validnost { get; set; }

    }
}
