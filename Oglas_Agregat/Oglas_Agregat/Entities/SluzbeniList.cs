using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Entities
{
    public class SluzbeniList
    {
        public Guid SluzbeniListId { get; set; }
        public DateTime DatumIzdanja { get; set; }
        public int BrojLista { get; set; }
        public List<Oglas> ListaOglasa { get; set; }

    }
}
