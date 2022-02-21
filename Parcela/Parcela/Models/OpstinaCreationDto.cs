using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje opstina
    /// </summary>
    public class OpstinaCreationDto
    {
        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NazivOpstine { get; set; }
    }

    /// <summary>
    /// Validator za kreiranje opstina
    /// </summary>
    public class OpstinaCreationValidator : AbstractValidator<OpstinaCreationDto>
    {
        /// <summary>
        /// Konstruktor validatora kreiranje za opstine
        /// </summary>
        public OpstinaCreationValidator()
        {
            List<string> conditions = new List<string>() { "Čantavir", "Bački Vinogradi", "Bikovo", "Đuđin", "Žednik", "Tavankut", "Bajmok", "Donji Grad", "Stari Grad", "Novi Grad", "Palić" };
            RuleFor(x => x.NazivOpstine).Must(x => conditions.Contains(x)).WithMessage("Opstina moze biti: " + String.Join(",", conditions));
        }
    }
}
