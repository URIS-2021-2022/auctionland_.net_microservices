using System;

namespace Komisija_Agregat.Models
{
    public class KomisijaModel
    {
        public Guid KomisijaId { get; set; }
        public PredsednikModel Predsednik { get; set; }
        public List<ClanKomisijeDto> Clanovi { get; set; }

    }
}

