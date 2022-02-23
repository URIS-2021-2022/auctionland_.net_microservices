using Liciter___Agregat.DTOs.Kupac;
using Liciter___Agregat.DTOs.OvlascenoLice;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Liciter
{
    public class LiciterDto
    {
        /// <summary>
        /// Kupac DTO
        /// </summary>
        public KupacDto Kupac { get; set; }

        /// <summary>
        /// Ovlašćeno lice DTO
        /// </summary>
        public OvlascenoLiceDto OvlascenoLice { get; set; }
    }
}
