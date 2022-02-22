using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Entities
{
    public class Komisija
    {
        [Key]
        public Guid KomisijaId { get; set; }   = Guid.NewGuid();
        public string Predsednik { get; set; }
        public string Clanovi { get; set; }
    }
}
