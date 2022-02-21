using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komisija_Agregat.Entities;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Data
{

    public class PredsednikRepository : IPredsednikRepository
    {
        public static List<Predsednik> Predsednici { get; set; } = new List<Predsednik>();

        public PredsednikRepository()
        {
            FillData();
        }

        private void FillData()
        {
            
        }

        public List<Predsednik> GetPredsednici(string ImePredsednika = null, string PrezimePredsednika = null, string EmailPredsednika = null)
        {

            return (from e in Predsednici
                    where string.IsNullOrEmpty(ImePredsednika) || e.ImePredsednika == ImePredsednika &&
                          string.IsNullOrEmpty(PrezimePredsednika) || e.PrezimePredsednika == PrezimePredsednika &&
                          string.IsNullOrEmpty(EmailPredsednika) || e.EmailPredsednika == EmailPredsednika
                    select e).ToList();
        }

        public Predsednik GetPredsednikById(Guid PredsednikId)
        {

            return Predsednici.FirstOrDefault(e => e.PredsednikId == PredsednikId);

        }

        public PredsednikConfirmation CreatePredsednik(Predsednik predsednik)
        {
            predsednik.PredsednikId = Guid.NewGuid();
            Predsednici.Add(predsednik);
            Predsednik pred = GetPredsednikById(predsednik.PredsednikId);

            return new PredsednikConfirmation
            {
                PredsednikId = pred.PredsednikId,

                ImePredsednika = pred.ImePredsednika,

                PrezimePredsednika = pred.PrezimePredsednika,

                EmailPredsednika = pred.EmailPredsednika,

            };
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
            Predsednici.Remove(Predsednici.FirstOrDefault(e => e.PredsednikId == PredsednikId));
        }

    }
}
