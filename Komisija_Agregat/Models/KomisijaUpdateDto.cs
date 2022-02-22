using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    public class KomisijaUpdateDto
    {
        /// <summary>
        /// DTO za azuriranje komisije
        /// </summary>
        public Guid KomisijaId { get; set; }
        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public string Predsednik { get; set; }
        /// <summary>
        /// Clanovi komisije
        /// </summary>
        public string Clanovi { get; set; }

    }
}

