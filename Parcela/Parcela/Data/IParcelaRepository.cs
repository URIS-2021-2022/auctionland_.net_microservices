using System;
using System.Collections.Generic;
using Parcela.Entities;

namespace Parcela.Data
{
    /// <summary>
    /// Interfejs za ParcelaRepository
    /// </summary>
    public interface IParcelaRepository
    {
        /// <summary>
        /// Metoda koja pribavlja podatke
        /// </summary>
        List<ParcelaM> GetParcele(string kultura);
        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        ParcelaM GetParcelaById(Guid parcelaId);
        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        ParcelaConfirmation CreateParcela(ParcelaM parcelaM);
        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        void UpdateParcela(ParcelaM parcelaM);
        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        void DeleteParcela(Guid parcelaId);
        /// <summary>
        /// Metoda koja cuva izmene
        /// </summary>
        bool SaveChanges();

    }
}
