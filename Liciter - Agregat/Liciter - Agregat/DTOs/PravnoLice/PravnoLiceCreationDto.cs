using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.PravnoLice
{
    public class PravnoLiceCreationDto
    {
        /// <summary>
        /// Naziv pravnog lica
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Maticni broj pravnog preduzeca
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
        /// Prvi broj telefona pravnog lica
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// Drugi broj telefona pravnog lica
        /// </summary>
        public string BrojTelefona2 { get; set; }

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
