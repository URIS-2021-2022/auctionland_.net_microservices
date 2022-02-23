using Licitacija_agregat.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    /// <summary>
    /// Predstavlja model kreiranja etape
    /// </summary>
    public class EtapaCreationDto : IValidatableObject
    {
        /// <summary>
        /// Dan etape
        /// </summary>
        [Required]
        public DateTime Dan { get; set; }

        /// <summary>
        /// Broj etape
        /// </summary>
        public int BrojEtape { get; set; }
        public Guid? LicitacijaId { get; set; }


        /// <summary>
        /// Potrebno je da broj etape bude pozitivan broj i da datum bude u budućnosti
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(BrojEtape < 1)
            {
                yield return new ValidationResult(
                    "The number of a stage has to be greater than 0.",
                    new[] { "EtapaCreationDto" });
            }

            if(Dan < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Date is required and it has to be a future date.",
                    new[] { "EtapaCreationDto" });
            }
        }
    }
}
