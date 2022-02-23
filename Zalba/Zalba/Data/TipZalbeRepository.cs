using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zalba.Entities;

namespace Zalba.Data
{
    /// <summary>
    /// Repozitorijum tipova zalbi
    /// </summary>
    public class TipZalbeRepository : ITipZalbeRepository
    {
        private readonly ZalbaContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor za repozitorijum tipova zalbi
        /// </summary>
        public TipZalbeRepository(ZalbaContext context, IMapper mapper)
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
        public List<TipZalbe> GetTipoveZalbi()
        {
            return context.TipoviZalbi.ToList();
        }

        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        public TipZalbe GetTipZalbeById(Guid tipZalbeId)
        {
            return context.TipoviZalbi.FirstOrDefault(e => e.TipZalbeId == tipZalbeId);
        }

        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        public TipZalbeConfirmation CreateTipZalbe(TipZalbe tipZalbe)
        {
            var createdEntity = context.Add(tipZalbe);
            return mapper.Map<TipZalbeConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        public void UpdateTipZalbe(TipZalbe tipZalbe)
        {
            //azurira se automatski
        }

        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        public void DeleteTipZalbe(Guid tipZalbeId)
        {
            var tipZalbe = GetTipZalbeById(tipZalbeId);
            context.Remove(tipZalbe);
        }
    }
}
