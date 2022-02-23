using System;
using System.Collections.Generic;
using Parcela.Entities;

namespace Parcela.Data
{
    /// <summary>
    /// Interfejs za ParcelaRepository
    /// </summary>
    public interface IDeoParceleRepository
    {
        /// <summary>
        /// Metoda koja pribavlja podatke
        /// </summary>
        List<DeoParcele> GetDeloveParcele();
        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        DeoParcele GetDeoParceleById (Guid deoParceleId);
        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        DeoParceleConfirmation CreateDeoParcele (DeoParcele deoParcele);
        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        void UpdateDeoParcele(DeoParcele deoParcele);
        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        void DeleteDeoParcele(Guid deoParceleId);
        /// <summary>
        /// Metoda koja cuva izmene
        /// </summary>
        bool SaveChanges();

    }
}
