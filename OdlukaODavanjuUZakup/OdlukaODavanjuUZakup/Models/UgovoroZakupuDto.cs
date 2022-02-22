using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Models
{
    /// <summary>
    /// Osnovni model ugovora o zakupu
    /// </summary>
    public class UgovoroZakupuDto
    {
        /// <summary>
        /// ID ugovora o zakupu
        /// </summary>
        public Guid UgovoroZakupuID { get; set; }
        /// <summary>
        /// Javno nadmetanje na koje se ugovor odnosi
        /// </summary>
        public string Javno_Nadmetanje { get; set; } //entitet
        /// <summary>
        /// Odluka, tip entiteta
        /// </summary>
        public string odluka { get; set; } //entitet
        /// <summary>
        /// Jemstvo, bankarska garancija, garancija nekretninom, zirantska, uplata gotovinom
        /// </summary>
        public string tip_garancije { get; set; }
        /// <summary>
        /// Lice koje potpisuje ugovor, entitet, moze biti pravno i fizicko
        /// </summary>
        public string lice { get; set; } //entitet

        //zakomentarisane treba da preuzmem od nekog drugog kao
        /// <summary>
        /// Rok dospeca ugovora
        /// </summary>
        public DateTime rokovi_dospeca { get; set; }
        /// <summary>
        /// Broj pod kojim je ugovor zaveden
        /// </summary>
        public string zavodni_Broj { get; set; }
        /// <summary>
        /// Datum kada je ugovor zaveden
        /// </summary>
        public DateTime datum_zavodjenja { get; set; }
        /// <summary>
        /// Krajnji rok kada se zemljiste vraca
        /// </summary>
        public DateTime rok_za_vracanje_zemljista { get; set; }
        /// <summary>
        /// Mesto u kome je ugovor potpisan
        /// </summary>
        public string mesto_potpisivanja { get; set; }
        /// <summary>
        /// Datum kada je ugovor potpisan
        /// </summary>
        public DateTime datum_potpisa { get; set; }

    }
}
