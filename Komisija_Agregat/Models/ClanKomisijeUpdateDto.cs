using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Models
{
    /// <summary>
    /// DTO za azuriranje clana komisije
    /// </summary>
    public class ClanKomisijeUpdateDto
    {

        public Guid ClanId { get; set; }
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

        public Guid KomisijaId { get; set; }
    }
}