﻿using Javno_Nadmetanje_Agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Models
{
    /// <summary>
    /// Predstavlja model tipa javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanjaDto
    {
        /// <summary>
        /// Id tipa javnog nadmetanja
        /// </summary>
        public Guid TipJavnogNadmetanjaId { get; set; }

        /// <summary>
        /// Naziv tipa javnog nadmetanja
        /// </summary>
        public string NazivTipaJavnogNadmetanja { get; set; }

        /// <summary>
        /// Lista javnih nadmetanja datog tipa
        /// </summary>
        public List<JavnoNadmetanje> ListaJavnihNadmetanja { get; set; }
    }
}
