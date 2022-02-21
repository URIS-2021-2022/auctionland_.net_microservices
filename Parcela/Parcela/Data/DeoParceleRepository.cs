using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Parcela.Entities;

namespace Parcela.Data
{
    /// <summary>
    /// Repozitorijum delova parcele
    /// </summary>
    public class DeoParceleRepository : IDeoParceleRepository
    {
        private readonly DeoParceleContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor za repozitorijum parceli
        /// </summary>
        public DeoParceleRepository(DeoParceleContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Metoda koja cuva izmene
        /// </summary>
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// Metoda koja pribavlja podatke
        /// </summary>
        public List<DeoParcele> GetDeloveParcele()
        {
            return context.DeloviParcele.ToList();
        }

        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        public DeoParcele GetDeoParceleById(Guid deoParceleId)
        {
            return context.DeloviParcele.FirstOrDefault(e => e.DeoParceleId == deoParceleId);
        }

        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        public DeoParceleConfirmation CreateDeoParcele(DeoParcele deoParcele)
        {
            var createdEntity = context.Add(deoParcele);
            return mapper.Map<DeoParceleConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        public void UpdateDeoParcele(DeoParcele deoParcele)
        {

        }

        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        public void DeleteDeoParcele(Guid deoParceleId)
        {
            var deoParcele = GetDeoParceleById(deoParceleId);
            context.Remove(deoParcele);
        }
    }
}
