using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.OvlascenoLice
{
    public class OvlascenoLiceDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string JMBG_Br_Pasosa { get; set; }
        public string Adresa { get; set; } //adresa je entitet
        public List<KupacModel> Kupac { get; set; }
        public string Drzava { get; set; }
        public List<int> BrojTable { get; set; }
    }
}
