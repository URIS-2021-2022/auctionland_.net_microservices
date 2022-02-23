using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za azuriranje opstina
    /// </summary>
    public class OpstinaUpdateDto
    {
        /// <summary>
        /// Id opstine
        /// </summary>
        public Guid OpstinaId { get; set; }
        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NazivOpstine { get; set; }
    }

    /// <summary>
    /// Validator za azuriranje opstina
    /// </summary>
    public class OpstinaUpdateValidator : AbstractValidator<OpstinaUpdateDto>
    {
        /// <summary>
        /// Konstruktor validatora za azuriranje opstina
        /// </summary>
        public OpstinaUpdateValidator()
        {
            List<string> conditions = new List<string>() { "Čantavir" , "Bački Vinogradi" , "Bikovo" , "Đuđin" , "Žednik" , "Tavankut" , "Bajmok" , "Donji Grad" , "Stari Grad" , "Novi Grad" , "Palić" };
            RuleFor(x => x.NazivOpstine).Must(x => conditions.Contains(x)).WithMessage("Opstina moze biti: " + String.Join(",", conditions));
        }
    }
}
