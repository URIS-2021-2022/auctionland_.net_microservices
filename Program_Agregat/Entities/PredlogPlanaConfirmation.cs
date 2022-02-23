using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;

namespace Program_Agregat.Entities
{
    public class PredlogPlanaConfirmation
    {
        [Key]
        public Guid PredlogPlanaId { get; set; }
        public string BrojDokumenta { get; set; }
    }
}