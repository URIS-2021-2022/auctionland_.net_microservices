using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Entities
{
    public class Komisija
    {
        public Guid KomisijaId { get; set; }
        public PredsednikDto Predsednik { get; set; }
        public List<ClanKomisijeDto> Clanovi { get; set; }
    }
}
