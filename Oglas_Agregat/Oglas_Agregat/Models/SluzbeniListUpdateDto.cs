using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    /// <summary>
    /// Predstavlja model za modifikaciju službenog lista
    /// </summary>
    public class SluzbeniListUpdateDto : IValidatableObject
    {
        /// <summary>
        /// Id službenog lista
        /// </summary>
        public Guid SluzbeniListId { get; set; }

        /// <summary>
        /// Datum izdavanja službenog lista
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti datum izdavanja službenog lista.")]
        public DateTime DatumIzdanja { get; set; }

        /// <summary>
        /// Broj lista
        /// </summary>
        public int BrojLista { get; set; }


        /// <summary>
        /// Validacija da uneti datum izdavanja službenog lista nije veći od današnjeg datuma
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
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
