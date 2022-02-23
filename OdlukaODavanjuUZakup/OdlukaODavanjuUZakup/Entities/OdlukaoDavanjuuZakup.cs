using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class OdlukaoDavanjuuZakup
    {
        [Key]
        public Guid OdlukaoDavanjuuZakupID { get; set; } = Guid.NewGuid();
        public DateTime datum_donosenja_odluke { get; set; }

        public Boolean validnost { get; set; }
    }
}
