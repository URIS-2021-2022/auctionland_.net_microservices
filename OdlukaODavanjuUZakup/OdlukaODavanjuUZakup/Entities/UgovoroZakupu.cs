using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class UgovoroZakupu
    {
        public Guid UgovoroZakupuID { get; set; }
        public string Javno_Nadmetanje { get; set; } //entitet
        public string odluka { get; set; } //entitet

        public GarantEnum tip_garancije { get; set; }

        public string lice { get; set; } //entitet

        //zakomentarisane treba da preuzmem od nekog drugog kao

        public DateTime[] rokovi_dospeca { get; set; }
        public string zavodni_Broj { get; set; }
        public DateTime datum_zavodjenja { get; set; }

        public DateTime rok_za_vracanje_zemljista { get; set; }

        public string mesto_potpisivanja { get; set; }

        public DateTime datum_potpisa { get; set; }
    }
}
