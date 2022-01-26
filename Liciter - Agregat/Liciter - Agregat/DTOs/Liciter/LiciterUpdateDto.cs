using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Liciter
{
    public class LiciterUpdateDto
    {
        public Guid LiciterId { get; set; }
        public KupacModel Kupac { get; set; }
        public OvlascenoLiceModel OvlascenoLice { get; set; }
    }
}
