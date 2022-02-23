using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Models
{
    /// <summary>
    /// DTO za azuriranje programa
    /// </summary>
    public class ProgramUpdateDto
    {
        public Guid ProgramId { get; set; }
        /// <summary>
        /// Maksimalno ogranicenje
        /// </summary>
        public string MaksimalnoOgranicenje { get; set; }
        /// <summary>
        /// Licitacije na koje se odnosi zadati program
        /// </summary>
        public string Licitacije { get; set; }


    }
}