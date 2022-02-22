using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Entities
{
    public class StatusJavnogNadmetanja
    {
        public Guid StatusJavnogNadmetanjaId { get; set; }
        public string NazivStatusaJavnogNadmetanja { get; set; }
        public List<JavnoNadmetanje> ListaJavnihNadmetanja { get; set; }
    }
}
