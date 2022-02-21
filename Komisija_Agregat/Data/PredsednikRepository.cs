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

    public class PredsednikRepository : IPredsednikRepository
    {
        private readonly KomisijaContext context;
        private readonly IMapper mapper;

        public PredsednikRepository(KomisijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges()>0;
        }

        public List<Predsednik> GetPredsednici(string ImePredsednika = null, string PrezimePredsednika = null, string EmailPredsednika = null)
        {

            return context.Predsednici.Where(e => string.IsNullOrEmpty(ImePredsednika) || e.ImePredsednika == ImePredsednika &&
                          string.IsNullOrEmpty(PrezimePredsednika) || e.PrezimePredsednika == PrezimePredsednika &&
                          string.IsNullOrEmpty(EmailPredsednika) || e.EmailPredsednika == EmailPredsednika).ToList();
        }

        public Predsednik GetPredsednikById(Guid PredsednikId)
        {

            return context.Predsednici.FirstOrDefault(e => e.PredsednikId == PredsednikId);

        }

        public PredsednikConfirmation CreatePredsednik(Predsednik predsednik)
        {
            var createdEntity = context.Add(predsednik);
            return mapper.Map<PredsednikConfirmation>(createdEntity.Entity);

        }

        public PredsednikConfirmation UpdatePredsednik(Predsednik predsednik)
        {
            Predsednik pred = GetPredsednikById(predsednik.PredsednikId);

            pred.PredsednikId = predsednik.PredsednikId;
            pred.ImePredsednika = predsednik.ImePredsednika;
            pred.PrezimePredsednika = predsednik.PrezimePredsednika;
            pred.EmailPredsednika = predsednik.EmailPredsednika;

            return new PredsednikConfirmation
            {
                PredsednikId = pred.PredsednikId,
                ImePredsednika = pred.ImePredsednika,
                PrezimePredsednika = pred.PrezimePredsednika,
                EmailPredsednika = pred.EmailPredsednika
            }; 
        }

        public void DeletePredsednik(Guid PredsednikId)
        {
            context.Remove(GetPredsednikById(PredsednikId));
        }

    }
}
