using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    public class KomisijaDto
    {
        public Guid KomisijaId { get; set; }
        public PredsednikDto Predsednik { get; set; }
        public List<ClanKomisijeDto> Clanovi { get; set; }

    }
}

