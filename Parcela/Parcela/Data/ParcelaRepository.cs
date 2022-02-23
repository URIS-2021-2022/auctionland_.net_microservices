using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Parcela.Data;
using Parcela.Entities;

namespace Parcela.Data
{
    /// <summary>
    /// Repozitorijum delova parcele
    /// </summary>
    public class ParcelaRepository : IParcelaRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor za repozitorijum delova parcele
        /// </summary>
        public ParcelaRepository(ParcelaContext context, IMapper mapper)
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
        public List<ParcelaM> GetParcele(string kultura)
        {
            //kultura = null;
            return context.Parcele.Where(e => (kultura == null || e.Kultura == kultura)).ToList();
        }

        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        public ParcelaM GetParcelaById(Guid parcelaId)
        {
            return context.Parcele.FirstOrDefault(e => e.ParcelaId == parcelaId);
        }

        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        public ParcelaConfirmation CreateParcela(ParcelaM parcelaM)
        {
            var createdEntity = context.Add(parcelaM);
            return mapper.Map<ParcelaConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        public void UpdateParcela(ParcelaM parcelaM)
        {
            //azurira se automatski
        }

        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        public void DeleteParcela(Guid parcelaId)
        {
            var parcelaM = GetParcelaById(parcelaId);
            context.Remove(parcelaM);
        }  
    }
}
