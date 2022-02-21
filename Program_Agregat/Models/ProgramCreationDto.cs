using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Models
{
    public class ProgramCreationDto
    {
        // public Guid ProgramId { get; set; }
        public string MaksimalnoOgranicenje { get; set; }
        public List<string> Licitacije { get; set; }


    }
}