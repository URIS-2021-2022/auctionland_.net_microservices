using Liciter___Agregat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Kupac
{
    public class KupacConfirmationDto
    {
        /// <summary>
        /// Prioritet kupca
        /// </summary>
        public Prioritet Prioritet { get; set; }

        /// <summary>
        /// Ostvarena površina kupca
        /// </summary>
        public int OstvarenaPovrsina { get; set; }

        /// <summary>
        /// Obeležje koje govori da li kupac ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }
    }
}
