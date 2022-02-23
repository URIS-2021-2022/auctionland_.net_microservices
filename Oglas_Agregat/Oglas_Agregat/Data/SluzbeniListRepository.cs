using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {

        private readonly OglasContext context;
        private readonly IMapper mapper;

        public SluzbeniListRepository(OglasContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public SluzbeniListConfirmation CreateSluzbeniList(SluzbeniList sluzbeniList)
        {
            var createdEntity = context.Add(sluzbeniList);
            return mapper.Map<SluzbeniListConfirmation>(createdEntity.Entity);
        }


        public void DeleteSluzbeniList(Guid SluzbeniListId)
        {
            context.Remove(GetSluzbeniListById(SluzbeniListId));
        }

        public SluzbeniList GetSluzbeniListById(Guid SluzbeniListId)
        {
            return context.SluzbeniListovi.FirstOrDefault(e => e.SluzbeniListId == SluzbeniListId);
        }

        public List<SluzbeniList> GetSluzbeniListovi(int BrojLista = default)
        {
            return context.SluzbeniListovi.Include(d => d.ListaOglasa).Where(e => (BrojLista == default || e.BrojLista.Equals(BrojLista))).ToList();

        }

        public void UpdateSluzbeniList(SluzbeniList sluzbeniList)
        {
            //nije potrebna implementacija
        }
    }
}
