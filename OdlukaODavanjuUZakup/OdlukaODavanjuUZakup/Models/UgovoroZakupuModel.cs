using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    public class UgovoroZakupuModel
    {
        public Guid UgovoroZakupuID { get; set; }
    //    public JAVNO_NADMETANJE Javno_Nadmetanje { get; set; }
    //    public ODLUKA odluka { get; set; }

        public GarantEnum tip_garancije { get; set; }

    //    public LICE lice { get; set; }

        //zakomentarisane treba da preuzmem od nekog drugog kao

        public DateTime[] rokovi_dospeca { get; set; }
        public string zavodni_Broj { get; set; }
        public DateTime datum_zavodjenja { get; set; }

        public DateTime rok_za_vracanje_zemljista { get; set;  }

        public string mesto_potpisivanja { get; set; }

        public DateTime datum_potpisa { get; set; }

    }
}
