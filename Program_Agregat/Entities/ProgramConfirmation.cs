using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;

namespace Program_Agregat.Entities
{
    public class ProgramConfirmation
    {
        [Key]
        public Guid ProgramId { get; set; }
    }
}