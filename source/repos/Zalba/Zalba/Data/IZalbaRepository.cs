using System;
using System.Collections.Generic;
using Zalba.Entities;

namespace Zalba.Data
{
    /// <summary>
    /// Interfejs za ZalbaRepository
    /// </summary>
    public interface IZalbaRepository
    {
        /// <summary>
        /// Metoda koja pribavlja podatke
        /// </summary>
        List<ZalbaM> GetZalbe(string statusZalbe);
        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        ZalbaM GetZalbaById(Guid zalbaId);
        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        ZalbaConfirmation CreateZalba(ZalbaM zalbaM);
        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        void UpdateZalba(ZalbaM zalbaM);
        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        void DeleteZalba(Guid zalbaId);
        /// <summary>
        /// Metoda koja cuva izmene
        /// </summary>
        bool SaveChanges();

    }
}
