using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    public class UplataZakupnineConfirmationDto
    {
        public Guid UplataZakupnineID { get; set; }
        public string broj_racuna { get; set; }

        public DateTime datum { get; set; }
    }
}
