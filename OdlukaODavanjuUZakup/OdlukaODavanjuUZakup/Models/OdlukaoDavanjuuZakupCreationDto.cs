using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    public class OdlukaoDavanjuuZakupCreationDto
    {
      //  public Guid OdlukaoDavanjuuZakupID { get; set; }
        public DateTime datum_donosenja_odluke { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti validnost odluke")]
        public Boolean validnost { get; set; }
    }
}
