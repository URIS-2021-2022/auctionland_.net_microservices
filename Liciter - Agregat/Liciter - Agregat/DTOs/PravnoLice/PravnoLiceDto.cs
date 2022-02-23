using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.PravnoLice
{
    public class PravnoLiceDto
    {
       /// <summary>
       /// Naziv pravnog lica
       /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Maticni broj pravnog lica
        /// </summary>
        public string MaticniBroj { get; set; }

        /// <summary>
        /// Kontakt osoba pravnog lica
        /// </summary>
        public string KontaktOsoba { get; set; } //kontakt osoba je entitet

        /// <summary>
        /// Adresa pravnog lica
        /// </summary>
        public string Adresa { get; set; } //adresa je entitet

        /// <summary>
        /// Brojevi telefona pravnog lica
        /// </summary>
        public string BrojeviTelefona { get; set; }

        /// <summary>
        /// Faks pravnog lica
        /// </summary>
        public string Faks { get; set; }

        /// <summary>
        /// Email pravnog lica
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Broj racuna pravnog lica
        /// </summary>
        public string BrojRacuna { get; set; }
    }
}
