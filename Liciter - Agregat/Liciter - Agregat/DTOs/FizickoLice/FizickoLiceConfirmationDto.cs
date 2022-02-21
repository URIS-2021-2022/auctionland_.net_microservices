using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.FizickoLice
{
    /// <summary>
    /// DTO za potvrdu fizickog lica
    /// </summary>
    public class FizickoLiceConfirmationDto
    {
        /// <summary>
        /// Ime fizickog lica
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime fizickog lica
        /// </summary>
        public string Prezime { get; set; }
    }
}
