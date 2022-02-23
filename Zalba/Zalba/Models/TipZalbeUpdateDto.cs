using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Zalba.Models
{
    /// <summary>
    /// DTO za azuriranje tipova zalbi
    /// </summary>
    public class TipZalbeUpdateDto
    {
        /// <summary>
        /// Id tipa zalbe
        /// </summary>
        public Guid TipZalbeId { get; set; }
        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string NazivTipa { get; set; }
    }

    /// <summary>
    /// Validator za azuriranje tipove zalbi
    /// </summary>
    public class TipZalbeUpdateValidator : AbstractValidator<TipZalbeUpdateDto>
    {
        /// <summary>
        /// Konstruktor validatora za azuriranje tipova zalbi
        /// </summary>
        public TipZalbeUpdateValidator()
        {
            List<string> conditions = new List<string>() { "Žalba na tok javnog nadmetanaja", "Žalba na Odluku o davanju u zakup", "Žalba na Odluku o davanju na korišćenje" };
            RuleFor(x => x.NazivTipa).Must(x => conditions.Contains(x)).WithMessage("Tip zalbe moze biti: " + String.Join(",", conditions));
        }
    }
}
