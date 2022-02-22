using Liciter___Agregat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Models
{
    public class KupacConfirmation
    {
        public Guid KupacId { get; set; }
        public PrioritetEnum Prioritet { get; set; }
        public int OstvarenaPovrsina { get; set; }
        public bool ImaZabranu { get; set; }

    }
}
