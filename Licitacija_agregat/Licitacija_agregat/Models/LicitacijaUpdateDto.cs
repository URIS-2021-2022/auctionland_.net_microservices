using Licitacija_agregat.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Models
{
    public class LicitacijaUpdateDto : IValidatableObject
    {
        /// <summary>
        /// Ključ licitacije
        /// </summary>
        public Guid LicitacijaId { get; set; }
        /// <summary>
        /// Broj licitacije
        /// </summary>
        public int Broj { get; set; }
        /// <summary>
        /// Godina licitacije
        /// </summary>
        public int Godina { get; set; }
        /// <summary>
        /// Datum licitacije
        /// </summary>
        public DateTime Datum { get; set; }
        /// <summary>
        /// Ograničenje licitacije
        /// </summary>
        public int Ogranicenje { get; set; }
        /// <summary>
        /// Korak uvećanja cene licitacije
        /// </summary>
        public int Korak_cene { get; set; }
        /// <summary>
        /// Lista koja sadrži dokumentaciju za fizička lica
        /// </summary>
        public List<string> Lista_dokumentacije_fizicka_lica { get; set; }
        /// <summary>
        /// Lista koja sadrži dokumentaciju za pravna lica
        /// </summary>
        public List<string> Lista_dokumentacije_pravna_lica { get; set; }
        /// <summary>
        /// Lista javnih nadmetanja
        /// </summary>
        public List<string> JavnoNadmetanje { get; set; }
        /// <summary>
        /// Lista etapa određene licitacije
        /// </summary>
        public List<Etapa> ListaEtapa { get; set; }

        /// <summary>
        /// Datum koji predstavlja rok za dostavljanje prijave
        /// </summary>
        public DateTime Rok_za_dostavljanje_prijave { get; set; }

        /// <summary>
        /// Broj licitacije i korak cene moraju da budu pozitivni, datum mora da bude u budućnost
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
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
