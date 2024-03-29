﻿using Licitacija_agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    /// <summary>
    /// Predstavlja model licitacije
    /// </summary>
    public class LicitacijaDto
    {
        public Guid ProgramId { get; set; }
        /// <summary>
        /// Id programa
        /// </summary>
        /// <summary>
        /// Broj licitacije
        /// </summary>
        public ProgramDto Program { get; set; }
        /// <summary>
        /// Program objekat
        /// </summary>
        public int Broj { get; set; }
        /// <summary>
        /// Godina licitacije
        /// </summary>
        public int Godina { get; set; }
        /// <summary>
        /// Datum licitacije
        /// </summary>
        public DateTime Datum { get; set; }
        /// <summary>
        /// Ograničenje licitacije
        /// </summary>
        public int Ogranicenje { get; set; }
        /// <summary>
        /// Korak uvećanja cene licitacije
        /// </summary>
        public int Korak_cene { get; set; }
        /// <summary>
        /// Lista koja sadrži dokumentaciju za fizička lica
        /// </summary>
        public List<string> Lista_dokumentacije_fizicka_lica { get; set; }
        /// <summary>
        /// Lista koja sadrži dokumentaciju za pravna lica
        /// </summary>
        public List<string> Lista_dokumentacije_pravna_lica { get; set; }
        /// <summary>
        /// Lista javnih nadmetanja
        /// </summary>
        public List<string> JavnoNadmetanje { get; set; } 
        /// <summary>
        /// Lista etapa određene licitacije
        /// </summary>
        public List<Etapa> ListaEtapa { get; set; }
        /// <summary>
        /// Datum koji predstavlja rok za dostavljanje prijave
        /// </summary>
        public DateTime Rok_za_dostavljanje_prijave { get; set; }



    }
}
