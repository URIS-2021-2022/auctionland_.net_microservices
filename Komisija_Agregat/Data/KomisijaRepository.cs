using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Entities;
using Komisija_Agregat.Models;
using AutoMapper;

namespace Komisija_Agregat.Data
{

    public class KomisijaRepository : IKomisijaRepository
    {
        private readonly KomisijaContext context;
        private readonly IMapper mapper;
        
        public KomisijaRepository(KomisijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Komisija> GetKomisije()
        {

            return context.Komisije.ToList();
        }

        public Komisija GetKomisijaById(Guid KomisijaId)
        {

            return context.Komisije.FirstOrDefault(e => e.KomisijaId == KomisijaId);

        }

        public KomisijaConfirmation CreateKomisija(Komisija komisija)
        {
            var createdEntity = context.Add(komisija);
            return mapper.Map<KomisijaConfirmation>(createdEntity.Entity);
            
        }

        public KomisijaConfirmation UpdateKomisija(Komisija komisija)
        {
           Komisija kom = GetKomisijaById(komisija.KomisijaId);

            kom.KomisijaId = komisija.KomisijaId;
            kom.Predsednik = komisija.Predsednik;
            kom.Clanovi = komisija.Clanovi;

            return new KomisijaConfirmation
            {
                KomisijaId = kom.KomisijaId,
                Predsednik = kom.Predsednik,              
            }; 
        }

        public void DeleteKomisija(Guid KomisijaId)
        {
            context.Remove(GetKomisijaById(KomisijaId));
        }

    }
}
