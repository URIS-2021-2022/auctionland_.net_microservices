using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// DTO za komisiju
    /// </summary>
    public class KomisijaDto
    {
        
        //public Guid KomisijaId { get; set; }
        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public PredsednikDto PredsednikKomisije { get; set; }
        /// <summary>
        /// Clanovi komisije
        /// </summary>
        public List<ClanKomisijeDto> Clanovi { get; set; }

    }
}
