using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class UgovoroZakupuConfirmation
    {
        public Guid UgovoroZakupuID { get; set; }
        public string zavodni_Broj { get; set; }
        public DateTime datum_potpisa { get; set; }
    }
}
