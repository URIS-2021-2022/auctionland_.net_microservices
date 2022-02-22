using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.OvlascenoLice
{
    public class OvlascenoLiceDto
    {
        /// <summary>
        /// Ime i prezime ovlašćenog lica
        /// </summary>
        public string Ime_Prezime { get; set; }

        /// <summary>
        /// JMBG ili Broj pasoša ovlašćenog lica
        /// </summary>
        public string JMBG_Br_Pasosa { get; set; }

        /// <summary>
        /// Adresa ovlašćenog lica
        /// </summary>
        public string Adresa { get; set; }
     
        /// <summary>
        /// Država ovlašćenog lica
        /// </summary>
        public string Drzava { get; set; }

        /// <summary>
        /// Lista brojeva tabele
        /// </summary>
        public List<int> BrojTable { get; set; }
    }
}
