using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.PravnoLice
{
    public class PravnoLiceCreationDto
    {
        public string Naziv { get; set; }
        public string MaticniBroj { get; set; }
        public string KontaktOsoba { get; set; } //kontakt osoba je entitet
        public string Adresa { get; set; } //adresa je entitet
        public string BrojTelefona1 { get; set; }
        public string BrojTelefona2 { get; set; }
        public string Faks { get; set; }
        public string Email { get; set; }
        public string BrojRacuna { get; set; }
    }
}
