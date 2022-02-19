using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    public class SluzbeniListUpdateDto : IValidatableObject
    {
        public Guid SluzbeniListId { get; set; }
        [Required(ErrorMessage = "Obavezno je uneti datum izdavanja službenog lista.")]
        public DateTime DatumIzdanja { get; set; }
        public int BrojLista { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (DatumIzdanja > DateTime.Today)
            {
                yield return new ValidationResult(
                   "Nije moguće za datum izdavanja službenog lista postaviti datum veći od današnjeg.",
                   new[] { "SluzbeniListUpdateDto" });
            }
        }
    }
}
