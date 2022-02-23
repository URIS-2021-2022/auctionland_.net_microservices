using System;
using System.Collections.Generic;
using Zalba.Entities;

namespace Zalba.Data
{
    /// <summary>
    /// Interfejs za ZalbaRepository
    /// </summary>
    public interface ITipZalbeRepository
    {
        /// <summary>
        /// Metoda koja pribavlja podatke
        /// </summary>
        List<TipZalbe> GetTipoveZalbi();
        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        TipZalbe GetTipZalbeById (Guid tipZalbeId);
        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        TipZalbeConfirmation CreateTipZalbe (TipZalbe tipZalbe);
        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        void UpdateTipZalbe(TipZalbe tipZalbe);
        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        void DeleteTipZalbe(Guid tipZalbeId);
        /// <summary>
        /// Metoda koja cuva izmene
        /// </summary>
        bool SaveChanges();

    }
}
