using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.FizickoLice
{
    /// <summary>
    /// DTO za azuriranje fizickog lica
    /// </summary>
    public class FizickoLiceUpdateDto
    {

        /// <summary>
        /// ID fizickog lica
        /// </summary>
        public Guid FizickoLiceId { get; set; }

        /// <summary>
        /// Ime fizickog lica
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime fizickog lica
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// JMBG fizickog lica
        /// </summary>
        public string JMBG { get; set; }

        /// <summary>
        /// Adresa fizickog lica
        /// </summary>
        public string Adresa { get; set; } //adresa je entitet

        /// <summary>
        /// Prvi broj telefona fizickog lica
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// Drugi broj telefona fizickog lica
        /// </summary>
        public string BrojTelefona2 { get; set; }

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
