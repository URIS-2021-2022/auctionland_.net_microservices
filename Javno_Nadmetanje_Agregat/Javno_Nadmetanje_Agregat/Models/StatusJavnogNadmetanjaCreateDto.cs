﻿using Javno_Nadmetanje_Agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Models
{
    /// <summary>
    /// Predstavlja model kreiranja statusa javnog nadmetanja
    /// </summary>
    public class StatusJavnogNadmetanjaCreateDto
    {
        /// <summary>
        /// Id statusa javnog nadmetanja
        /// </summary>
        public Guid StatusJavnogNadmetanjaId { get; set; }

        /// <summary>
        /// Naziv statusa javnog nadmetanja
        /// </summary>
        public string NazivStatusaJavnogNadmetanja { get; set; }

        /// <summary>
        /// Lista javnih nadmetanja datog statusa
        /// </summary>
        public List<JavnoNadmetanje> ListaJavnihNadmetanja { get; set; }
    }
}
