using AutoMapper;
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


        public SluzbeniListConfirmation CreateSluzbeniList(SluzbeniList sluzbeniListModel)
        {
            var createdEntity = context.Add(sluzbeniListModel);
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
            return context.SluzbeniListovi.Where(e => (BrojLista == default || e.BrojLista.Equals(BrojLista))).ToList();

        }

        public void UpdateSluzbeniList(SluzbeniList sluzbeniListModel)
        {
/*            var sluzbeniList = GetSluzbeniListById(sluzbeniListModel.SluzbeniListId);

            sluzbeniList.SluzbeniListId = sluzbeniListModel.SluzbeniListId;
            sluzbeniList.DatumIzdanja = sluzbeniListModel.DatumIzdanja;
            sluzbeniList.BrojLista = sluzbeniListModel.BrojLista;

            return new SluzbeniListConfirmation()
            {
                SluzbeniListId = sluzbeniList.SluzbeniListId,
                BrojLista = sluzbeniList.BrojLista
            };*/
        }
    }
}
