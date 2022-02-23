using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Zalba.Models
{
    /// <summary>
    /// DTO za azuriranje zalbi
    /// </summary>
    public class ZalbaUpdateDto
    {
        /// <summary>
        /// ID zalbe
        /// </summary>
        public Guid ZalbaId { get; set; }
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid TipId { get; set; }
        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        public DateTime DatumPodnosenjaZalbe { get; set; }
        /// <summary>
        /// ID podnosioca zalbe
        /// </summary>
        public Guid? PodnosilacZalbe { get; set; }
        /// <summary>
        /// Razlog zalbe zalbe
        /// </summary>
        public string RazlogZalbe { get; set; }
        /// <summary>
        /// Obrazlozenje zalbe
        /// </summary>
        public string Obrazlozenje { get; set; }
        /// <summary>
        /// Datum resenja zalbe
        /// </summary>
        public DateTime DatumResenja { get; set; }
        /// <summary>
        /// Broj resenja zalbe
        /// </summary>
        public string BrojResenja { get; set; }
        /// <summary>
        /// Status zalbe
        /// </summary>
        public string StatusZalbe { get; set; }
        /// <summary>
        /// Broj odluke zalbe
        /// </summary>
        public string BrojOdluke { get; set; }
        /// <summary>
        /// Radnja na osnovu zalbe
        /// </summary>
        public string RadnjaNaOsnovuZalbe { get; set; }
    }

    /// <summary>
    /// Validator za azuriranje zalbi
    /// </summary>
    public class ZalbaUpdateValidator : AbstractValidator<ZalbaUpdateDto>
    {
        /// <summary>
        /// Konstruktor validatora za azuriranje zalbi
        /// </summary>
        public ZalbaUpdateValidator()
        {
            List<string> conditions1 = new List<string>() { "JN ide u drugi krug sa novim uslovima", "JN ide u drugi krug sa starim uslovima", "JN ne ide u drugi krug" };
            List<string> conditions2 = new List<string>() { "Usvojena", "Odbijena", "Otvorena" };

            RuleFor(x => x.TipId).NotEmpty().WithMessage("Tip zalbe ne moze biti prazan");
            RuleFor(x => x.DatumPodnosenjaZalbe).NotEmpty().WithMessage("Datm podnosenja zalbe ne moze biti prazan");

            RuleFor(x => x.RazlogZalbe).NotEmpty().WithMessage("Razlog zalbe ne moze biti prazan");
            RuleFor(x => x.DatumResenja).NotEmpty().WithMessage("Datum resenja ne moze biti prazan");
            RuleFor(x => x.Obrazlozenje).NotEmpty().WithMessage("Obrazlozenje ne moze biti prazno");
            RuleFor(x => x.BrojResenja).NotEmpty().WithMessage("Broj resenja ne moze biti prazan");
            RuleFor(x => x.StatusZalbe).Must(x => conditions2.Contains(x)).WithMessage("Status zalbe moze biti: " + String.Join(",", conditions2));
            RuleFor(x => x.BrojOdluke).NotEmpty().WithMessage("Broj odluke ne moze biti prazan");
            RuleFor(x => x.RadnjaNaOsnovuZalbe).Must(x => conditions1.Contains(x)).WithMessage("Radnja na osnovu zalbe moze biti: " + String.Join(",", conditions1));
        }
    }
}
