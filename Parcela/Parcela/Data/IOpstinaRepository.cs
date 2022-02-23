using System;
using System.Collections.Generic;
using Parcela.Entities;

namespace Parcela.Data
{
    /// <summary>
    /// Interfejs za OpstinaRepository
    /// </summary>
    public interface IOpstinaRepository
    {
        /// <summary>
        /// Metoda koja pribavlja podatke
        /// </summary>
        List<Opstina> GetOpstine();
        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        Opstina GetOpstinaById(Guid opstinaId);
        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        OpstinaConfirmation CreateOpstina(Opstina opstina);
        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        void UpdateOpstina(Opstina opstina);
        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        void DeleteOpstina(Guid opstinaId);
        /// <summary>
        /// Metoda koja cuva izmene
        /// </summary>
        bool SaveChanges();

    }
}
