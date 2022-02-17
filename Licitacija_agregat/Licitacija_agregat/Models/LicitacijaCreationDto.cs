using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    public class LicitacijaCreationDto : IValidatableObject
    {
        public int Broj { get; set; }
        public int Godina { get; set; }
        public DateTime Datum { get; set; }
        public int Ogranicenje { get; set; }
        public int Korak_cene { get; set; }
        public List<string> Lista_dokumentacije_fizicka_lica { get; set; }
        public List<string> Lista_dokumentacije_pravna_lica { get; set; }
        public List<string> JavnoNadmetanje { get; set; } // tudji servis
        public DateTime Rok_za_dostavljanje_prijave { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Broj < 1)
            {
                yield return new ValidationResult(
                    "The number has to be greater than 0.",
                    new[] { "LicitacijaCreationDto" });
            }

            if (Korak_cene < 1)
            {
                yield return new ValidationResult(
                    "The value for the increase of price has to be greater than 0.",
                    new[] { "LicitacijaCreationDto" });
            }

            if (Datum < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Date is required and it has to be a future date.",
                    new[] { "LicitacijaCreationDto" });
            }
        }
    }
}
