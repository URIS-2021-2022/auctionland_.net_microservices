using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Models
{
    public class LiciterModel
    {
        public Guid LiciterId { get; set; }
        public KupacModel Kupac { get; set; }
        public OvlascenoLiceModel OvlascenoLice { get; set; }
    }
}
