using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Liciter
{
    public class LiciterCreationDto
    {
        /// <summary>
        /// Id kupca
        /// </summary>
        public Guid? KupacId { get; set; }

        /// <summary>
        /// Id ovlašćenog lica
        /// </summary>
        public Guid? OvlascenoLiceId { get; set; }
    }
}
