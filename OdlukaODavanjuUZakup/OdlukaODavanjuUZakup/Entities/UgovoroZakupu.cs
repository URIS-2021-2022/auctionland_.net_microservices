using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class UgovoroZakupu
    {
        [Key]
        public Guid UgovoroZakupuID { get; set; } = Guid.NewGuid();
        public string Javno_Nadmetanje { get; set; } //entitet
        public string odluka { get; set; } //entitet

        public string tip_garancije { get; set; }

        public string lice { get; set; } //entitet

        //zakomentarisane treba da preuzmem od nekog drugog kao

        public DateTime rokovi_dospeca { get; set; }
        public string zavodni_Broj { get; set; }
        public DateTime datum_zavodjenja { get; set; }

        public DateTime rok_za_vracanje_zemljista { get; set; }

        public string mesto_potpisivanja { get; set; }

        public DateTime datum_potpisa { get; set; }
    }
}
