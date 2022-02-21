using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.FizickoLice
{
    /// <summary>
    /// DTO za fizicko lica
    /// </summary>
    public class FizickoLiceDto
    {

        /// <summary>
        /// Ime i prezime fizickog lica
        /// </summary>
        public string Ime_Prezime { get; set; }

        /// <summary>
        /// JMBG fizickog lica
        /// </summary>
        public string JMBG { get; set; }

        /// <summary>
        /// Adresa fizickog lica
        /// </summary>
        public string Adresa { get; set; } //adresa je entitet

        /// <summary>
        /// Brojevi telefona fizickog lica
        /// </summary>
        public string BrojeviTelefona { get; set; }


        /// <summary>
        /// Email fizickog lica
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Broj racuna fizickog lica
        /// </summary>
        public string BrojRacuna { get; set; }
    }
}
