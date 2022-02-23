using System;

namespace Parcela.Models
{
    public class FizickoLiceDto
    {
        /// <summary>
        /// Id fizickog lica
        /// </summary>
        public Guid FizickoLiceId { get; set; }
        /// <summary>
        /// Ime i prezime fizickog lica
        /// </summary>
        public string Ime_Prezime { get; set; }

        /// <summary>
        /// JMBG fizickog lica
        /// </summary>
        public string JMBG { get; set; }

        /// <summary>
        /// Adresa fizickog lica
        /// </summary>
        public string Adresa { get; set; }

        /// <summary>
        /// Brojevi telefona fizickog lica
        /// </summary>
        public string BrojeviTelefona { get; set; }


        /// <summary>
        /// Email fizickog lica
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Broj racuna fizickog lica
        /// </summary>
        public string BrojRacuna { get; set; }

    }
}
