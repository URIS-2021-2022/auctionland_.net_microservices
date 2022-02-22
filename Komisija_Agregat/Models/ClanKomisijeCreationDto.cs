using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// Model za kreiranje clana komisije
    /// </summary>
    public class ClanKomisijeCreationDto
    {
        // public Guid ClanId { get; set; }

        /// <summary>
        /// Ime clana komisije
        /// </summary>
        public string ImeClana { get; set; }
        /// <summary>
        /// Prezime clana komisije
        /// </summary>
        public string PrezimeClana { get; set; }
        /// <summary>
        /// Email adresa clana komisije
        /// </summary>
        public string EmailClana { get; set; }
    }
}

