using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    public class EtapaModel
    {
        public Guid EtapaId { get; set; }
        public DateTime Dan { get; set; }
        public int BrojEtape { get; set; }
    }
}
