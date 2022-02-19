using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    public class OglasCreateDto : IValidatableObject
    {
        [Required(ErrorMessage ="Obavezno je uneti datum objave oglasa.")]
        public DateTime DatumObjave { get; set; }
        [MaxLength(100, ErrorMessage = "Opis oglasa ne sme da prevaziđe 100 karaktera.")]
        public string OpisOglasa { get; set; }
        [Required]
        public DateTime RokZaZalbu { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (DatumObjave > DateTime.Today)
            {
                yield return new ValidationResult(
                   "Nije moguće za datum objave oglasa postaviti datum veći od današnjeg.",
                   new[] { "OglasCreateDto" });
            }
        }
    }
}
