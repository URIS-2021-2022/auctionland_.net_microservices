using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Parcela.Entities;

namespace Parcela.Data
{
    /// <summary>
    /// Repozitorijum opstina
    /// </summary>
    public class OpstinaRepository : IOpstinaRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor za repozitorijum opstina
        /// </summary>
        public OpstinaRepository(ParcelaContext context, IMapper mapper)
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
        public List<Opstina> GetOpstine()
        {
            return context.Opstine.ToList();
        }

        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        public Opstina GetOpstinaById(Guid opstinaId)
        {
            return context.Opstine.FirstOrDefault(e => e.OpstinaId == opstinaId);
        }

        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        public OpstinaConfirmation CreateOpstina(Opstina opstina)
        {
            var createdEntity = context.Add(opstina);
            return mapper.Map<OpstinaConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        public void UpdateOpstina(Opstina opstina)
        {
            //azurira se automatski
        }

        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        public void DeleteOpstina(Guid opstinaId)
        {
            var opstina = GetOpstinaById(opstinaId);
            context.Remove(opstina);
        }
    }
}
