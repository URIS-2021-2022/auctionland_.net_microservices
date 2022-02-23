using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Models
{
    /// <summary>
    /// DTO za clana komisije
    /// </summary>
    public class ClanKomisijeDto
    {

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