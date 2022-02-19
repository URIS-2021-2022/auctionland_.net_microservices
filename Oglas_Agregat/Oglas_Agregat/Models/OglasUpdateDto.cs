﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    /// <summary>
    /// Predstavlja model za modifikaciju oglasa
    /// </summary>
    public class OglasUpdateDto : IValidatableObject
    {
        /// <summary>
        /// Id oglasa
        /// </summary>
        public Guid OglasId { get; set; }

        /// <summary>
        /// Datum objave oglasa
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti datum objave oglasa.")]
        public DateTime DatumObjave { get; set; }

        /// <summary>
        /// Opis oglasa
        /// </summary>
        [MaxLength(100, ErrorMessage = "Opis oglasa ne sme da prevaziđe 100 karaktera.")]
        public string OpisOglasa { get; set; }

        /// <summary>
        /// Rok za žalbu na oglas
        /// </summary>
        public DateTime RokZaZalbu { get; set; }

        /// <summary>
        /// Validacija da uneti datum objave oglasa nije veći od današnjeg datuma
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
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
