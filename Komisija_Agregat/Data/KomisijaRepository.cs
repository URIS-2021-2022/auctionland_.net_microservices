using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Entities;
using Komisija_Agregat.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

            return context.Komisije.Include(p => p.PredsednikKomisije).Include(c => c.Clanovi).ToList();
        }

        public Komisija GetKomisijaById(Guid KomisijaId)
        {

            return context.Komisije.Include(p=>p.PredsednikKomisije).Include(c=>c.Clanovi).FirstOrDefault(e => e.KomisijaId == KomisijaId);

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
            kom.PredsednikKomisije = komisija.PredsednikKomisije;
            kom.Clanovi = komisija.Clanovi;
            kom.PredsednikId = komisija.PredsednikId;

            return new KomisijaConfirmation
            {
                KomisijaId = kom.KomisijaId,
                PredsednikKomisije = kom.PredsednikKomisije,
            };
        }

        public void DeleteKomisija(Guid KomisijaId)
        {
            context.Remove(GetKomisijaById(KomisijaId));
        }

    }
}