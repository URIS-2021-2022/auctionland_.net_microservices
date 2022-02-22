using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// Model za kreiranje komisije
    /// </summary>
    public class KomisijaCreationDto
    {
        //public Guid KomisijaId { get; set; }

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

