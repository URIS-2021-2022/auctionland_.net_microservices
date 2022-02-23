using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zalba.Data;
using Zalba.Entities;

namespace Zalba.Data
{
    /// <summary>
    /// Repozitorijum tipova zalbi
    /// </summary>
    public class ZalbaRepository : IZalbaRepository
    {
        private readonly ZalbaContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor za repozitorijum tipova zalbi
        /// </summary>
        public ZalbaRepository(ZalbaContext context, IMapper mapper)
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
        public List<ZalbaM> GetZalbe(string brojResenja = null)
        {
            return context.Zalbe.Where(e => (brojResenja == null || e.BrojResenja == brojResenja)).ToList();
        }

        /// <summary>
        /// Metoda koja pribavlja podatke po ID-u
        /// </summary>
        public ZalbaM GetZalbaById(Guid zalbaId)
        {
            return context.Zalbe.FirstOrDefault(e => e.ZalbaId == zalbaId);
        }

        /// <summary>
        /// Metoda koja upisuje podatke
        /// </summary>
        public ZalbaConfirmation CreateZalba(ZalbaM zalbaM)
        {
            var createdEntity = context.Add(zalbaM);
            return mapper.Map<ZalbaConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Metoda koja azurira podatke
        /// </summary>
        public void UpdateZalba(ZalbaM zalbaM)
        {

        }

        /// <summary>
        /// Metoda koja brise podatke
        /// </summary>
        public void DeleteZalba(Guid zalbaId)
        {
            var zalbaM = GetZalbaById(zalbaId);
            context.Remove(zalbaM);
        }  
    }
}
