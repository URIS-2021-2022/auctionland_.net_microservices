using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Zalba.Models
{
    /// <summary>
    /// DTO za kreiranje tipova zalbi
    /// </summary>
    public class TipZalbeCreationDto
    {
        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string NazivTipa { get; set; }
    }

    /// <summary>
    /// Validator za kreiranje tipova zalbi
    /// </summary>
    public class TipZalbeCreationValidator : AbstractValidator<TipZalbeCreationDto>
    {
        /// <summary>
        /// Konstruktor validatora kreiranje za tipova zalbi
        /// </summary>
        public TipZalbeCreationValidator()
        {
            List<string> conditions = new List<string>() { "Žalba na tok javnog nadmetanaja", "Žalba na Odluku o davanju u zakup", "Žalba na Odluku o davanju na korišćenje" };
            RuleFor(x => x.NazivTipa).Must(x => conditions.Contains(x)).WithMessage("Tip zalbe moze biti: " + String.Join(",", conditions));
        }
    }
}
