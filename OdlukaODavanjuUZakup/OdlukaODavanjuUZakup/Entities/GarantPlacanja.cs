using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class GarantPlacanja
    {
        [Key]
        public Guid GarantPlacanjaID { get; set; } = Guid.NewGuid();
        public string Opis_garanta1 { get; set; }
        public string Opis_garanta2 { get; set; }

        public UgovoroZakupu ugovorOZakupu { get; set; }
        public Guid? UgovorOZakupuID { get; set; }
    }
}
