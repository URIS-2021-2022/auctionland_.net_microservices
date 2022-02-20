using System;

namespace Program_Agregat.Models
{
    public class ProgramModel
    {
        public Guid ProgramId { get; set; }
        public int MaksimalnoOgranicenje { get; set; }
        public List <string> Licitacije { get; set; }


    }
}