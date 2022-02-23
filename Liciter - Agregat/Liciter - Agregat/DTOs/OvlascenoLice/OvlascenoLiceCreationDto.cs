using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.OvlascenoLice
{
    public class OvlascenoLiceCreationDto
    {
        /// <summary>
        /// Ime ovlašćenog lica
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime ovlašćenog lica
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// JMBG ili broj pasoša ovlašćenog lica
        /// </summary>
        public string JMBG_Br_Pasosa { get; set; }

        /// <summary>
        /// Adresa ovlašćenog lica
        /// </summary>
        public string Adresa { get; set; }

        /// <summary>
        /// Id kupca
        /// </summary>
        public Guid KupacId { get; set; }

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
