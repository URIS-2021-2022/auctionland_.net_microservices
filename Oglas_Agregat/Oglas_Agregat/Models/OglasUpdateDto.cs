using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    public class OglasUpdateDto : IValidatableObject
    {
        public Guid OglasId { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti datum objave oglasa.")]
        public DateTime DatumObjave { get; set; }

        [MaxLength(100, ErrorMessage = "Opis oglasa ne sme da prevaziđe 100 karaktera.")]
        public string OpisOglasa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (DatumObjave > DateTime.Today)
            {
                yield return new ValidationResult(
                   "Nije moguće za datum objave oglasa postaviti datum veći od današnjeg.",
                   new[] { "OglasUpdateDto" });
            }
        }

    }
}
