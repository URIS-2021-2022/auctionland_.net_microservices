using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcela.Entities
{
    /// <summary>
    /// Enitet parcela
    /// </summary>
    public class ParcelaM
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        [Key]
        public Guid ParcelaId { get; set; }
        /// <summary>
        /// Povrsina parcele
        /// </summary>
        public int Povrsina { get; set; }
        /// <summary>
        /// ID korisnika parcele
        /// </summary>
        public Guid? KorisnikParcele { get; set; }
        /// <summary>
        /// Broj parcele
        /// </summary>
        public string BrojParcele { get; set; }
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        [ForeignKey("FK_Opstina")]
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
        /// <summary>
        /// Lista delova parcele
        /// </summary>
        public List<DeoParcele> DeloviParcele { get; set; }
    }
}
