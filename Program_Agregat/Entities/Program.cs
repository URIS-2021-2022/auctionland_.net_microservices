using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;

namespace Program_Agregat.Entities
{
    public class Program
    {
        public Guid ProgramId { get; set; }
        public string MaksimalnoOgranicenje { get; set; }
        public List <string> Licitacije { get; set; }


    }
}