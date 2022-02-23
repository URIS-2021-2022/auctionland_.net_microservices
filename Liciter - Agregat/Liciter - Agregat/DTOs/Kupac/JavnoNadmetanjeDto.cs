using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.DTOs.Kupac
{
    public class JavnoNadmetanjeDto
    {
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeId { get; set; }

        /// <summary>
        /// Id tipa javnog nadmetanja
        /// </summary>
        public Guid TipJavnogNadmetanjaId { get; set; }

        /// <summary>
        /// Id statusa javnog nadmetanja
        /// </summary>
        public Guid StatusJavnogNadmetanjaId { get; set; }

        /// <summary>
        /// Datum javnog nadmetanja
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Vreme pocetka javnog nadmetanja
        /// </summary>
        public DateTime VremePocetka { get; set; }

        /// <summary>
        /// Vreme zavrsetka javnog nadmetanja
        /// </summary>
        public DateTime VremeKraja { get; set; }

        /// <summary>
        /// Pocetna cena hektara za jednu godinu zakupa
        /// </summary>
        public int PocetnaCenaHektar { get; set; }

        /// <summary>
        /// Da li je javno nadmetanje predvidjeno programom ali je uklonjeno iz licitacije
        /// </summary>
        public bool Izuzeto { get; set; }

        /// <summary>
        /// Izlicitirana cena
        /// </summary>
        public int IzlicitiranaCena { get; set; }

        /// <summary>
        /// Period zakupa
        /// </summary>
        public int PeriodZakupa { get; set; }

        /// <summary>
        /// Broj ucesnika u nadmetanju
        /// </summary>
        public int BrojUcesnika { get; set; }

        /// <summary>
        /// Visina dopune depozita
        /// </summary>
        public int VisinaDopuneDepozita { get; set; }

        /// <summary>
        /// Krug javnog nadmetanja
        /// </summary>
        public int Krug { get; set; }
    }
}
