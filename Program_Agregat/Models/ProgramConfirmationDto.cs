using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Models
{
    public class ProgramConfirmationDto
    {
        public Guid ProgramId { get; set; }
        public string MaksimalnoOgranicenje { get; set; }
    }
}