using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za azuriranje parcela
    /// </summary>
    public class ParcelaUpdateDto
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelaId { get; set; }
        /// <summary>
        /// Povrsina parcele
        /// </summary>
        public int Povrsina { get; set; }
        /*/// <summary>
        /// ID korisnika parcele
        /// </summary>
        public Guid KorisnikParcele { get; set; }*/
        /// <summary>
        /// Broj parcele
        /// </summary>
        public string BrojParcele { get; set; }
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstina { get; set; }
        /// <summary>
        /// Broj lista nepokretnosti parcele
        /// </summary>
        public string BrojListaNepokretnosti { get; set; }
        /// <summary>
        /// Kultura parcele
        /// </summary>
        public string Kultura { get; set; }
        /// <summary>
        /// Klasa parcele
        /// </summary>
        public string Klasa { get; set; }
        /// <summary>
        /// Obradivost parcele
        /// </summary>
        public string Obradivost { get; set; }
        /// <summary>
        /// Zasticena zona parcele
        /// </summary>
        public string ZasticenaZona { get; set; }
        /// <summary>
        /// Oblik svojine parcele
        /// </summary>
        public string OblikSvojine { get; set; }
        /// <summary>
        /// Odvodnjavanje parcele
        /// </summary>
        public string Odvodnjavanje { get; set; }
        /// <summary>
        /// Stvarno stanje kulture parcele
        /// </summary>
        public string KulturaStvarnoStanje { get; set; }
        /// <summary>
        /// Stvarno stanje klase parcele
        /// </summary>
        public string KlasaStvarnoStanje { get; set; }
        /// <summary>
        /// Stvarno stanje obradivosti parcele
        /// </summary>
        public string ObradivostStvarnoStanje { get; set; }
        /// <summary>
        /// Stvarno stanje zasticene zone parcele
        /// </summary>
        public string ZasticenaZonaStvarnoStanje { get; set; }
        /// <summary>
        /// Stvarno stanje odvodanjavanja parcele
        /// </summary>
        public string OdvodnjavanjeStvarnoStanje { get; set; }
    }

    /// <summary>
    /// Validator za azuriranje parceli
    /// </summary>
    public class ParcelaUpdateValidator : AbstractValidator<ParcelaUpdateDto>
    {
        /// <summary>
        /// Konstruktor validatora za azuriranje parceli
        /// </summary>
        public ParcelaUpdateValidator()
        {
            List<string> conditions1 = new List<string>() { "Njive", "Vrtovi", "Voćnjaci", "Vinogradi", "Livade", "Pašnjaci", "Šume", "Trstici-močvare" };
            List<string> conditions2 = new List<string>() { "I", "II", "III", "IV", "V", "VI", "VII", "VIII" };
            List<string> conditions3 = new List<string>() { "Obradivo", "Ostalo" };
            List<string> conditions4 = new List<string>() { "1", "2", "3", "4" };
            List<string> conditions5 = new List<string>() { "Privatna svojina", "Državna svojina RS", "Državna svojina", "Društvena svojina", "Zadružna svojina", "Mešovita svojina", "Drugi oblici" };

            RuleFor(x => x.Povrsina).NotEmpty().WithMessage("Povrsina parcele mora biti unesena");
            //RuleFor(x => x.KorisnikParcele).NotEmpty().WithMessage("Podnosilac parcele mora biti unesen");
            RuleFor(x => x.BrojParcele).NotEmpty().WithMessage("Broj parcele mora biti unesen");
            RuleFor(x => x.KatastarskaOpstina).NotEmpty().WithMessage("Katastarska opstina parcele mora biti unesena");
            RuleFor(x => x.BrojListaNepokretnosti).NotEmpty().WithMessage("Broj lista nepokretnosti parcele mora biti unesen");
            RuleFor(x => x.Kultura).Must(x => conditions1.Contains(x)).WithMessage("Kultura parcele moze biti: " + String.Join(",", conditions1));
            RuleFor(x => x.Klasa).Must(x => conditions2.Contains(x)).WithMessage("Klasa parcele moze biti: " + String.Join(",", conditions2));
            RuleFor(x => x.Obradivost).Must(x => conditions3.Contains(x)).WithMessage("Obradivost parcele moze biti: " + String.Join(",", conditions3));
            RuleFor(x => x.ZasticenaZona).Must(x => conditions4.Contains(x)).WithMessage("Zasticena zona parcele moze biti: " + String.Join(",", conditions4));
            RuleFor(x => x.OblikSvojine).Must(x => conditions5.Contains(x)).WithMessage("Onlik svojine parcele moze biti: " + String.Join(",", conditions5));
            RuleFor(x => x.Odvodnjavanje).NotEmpty().WithMessage("Odvodnjavanje parcele mora biti uneseno");
            RuleFor(x => x.KulturaStvarnoStanje).NotEmpty().WithMessage("Stvarno stanje kulture parcele mora biti uneseno");
            RuleFor(x => x.KlasaStvarnoStanje).NotEmpty().WithMessage("Stvarno stanje klase parcele mora biti uneseno");
            RuleFor(x => x.ObradivostStvarnoStanje).NotEmpty().WithMessage("Stvarno stanje obradivosti parcele mora biti uneseno");
            RuleFor(x => x.ZasticenaZonaStvarnoStanje).NotEmpty().WithMessage("Stvarno stanje zasticene zone parcele mora biti uneseno");
            RuleFor(x => x.OdvodnjavanjeStvarnoStanje).NotEmpty().WithMessage("Stvarno stanje odvodnjavanja parcele mora biti uneseno");
        }
    }
}
