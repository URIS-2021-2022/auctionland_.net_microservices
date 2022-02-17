using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    public class EtapaCreationDto : IValidatableObject
    {
        public DateTime Dan { get; set; }

        public int BrojEtape { get; set; }

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
