using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje delova parcele
    /// </summary>
    public class DeoParceleCreationDto
    {
        /// <summary>
        /// Id parcele
        /// </summary>
        public Guid ParcelaId { get; set; }
        /// <summary>
        /// Naziv dela parcele
        /// </summary>
        public string PovrsinaDelaParcele { get; set; }
        /// <summary>
        /// Lista parceli
        /// </summary>
        public int RbrDelaParcele { get; set; }
    }

    /// <summary>
    /// Validator za kreiranje delova parcele
    /// </summary>
    public class DeoParceleCreationValidator : AbstractValidator<DeoParceleCreationDto>
    {
        /// <summary>
        /// Konstruktor validatora kreiranje za delova parcele
        /// </summary>
        public DeoParceleCreationValidator()
        {
            RuleFor(x => x.ParcelaId).NotEmpty().WithMessage("Id parcele mora biti unesen");
            RuleFor(x => x.PovrsinaDelaParcele).NotEmpty().WithMessage("Povrsina dela parcele mora biti unesena");
            RuleFor(x => x.RbrDelaParcele).NotEmpty().WithMessage("Redni broj dela parcele mora biti unesen");
        }
    }
}
