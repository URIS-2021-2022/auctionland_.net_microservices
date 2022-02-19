using System;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Data
{

    public class PredsednikRepository : IPredsednikRepository
    {
        public static List<PredsednikModel> Predsednici { get; set; } = new List<PredsednikModel>();

        public PredsednikRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Predsednici.AddRange(new List<PredsednikModel>
            {
                new PredsednikModel
                {
                   PredsednikId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                   ImePredsednika = "Petar",
                   PrezimePredsednika = "Marković",
                   EmailPredsednika = "markuza@mail.com"
                },
                new PredsednikModel
                {
                   PredsednikId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   ImePredsednika = "Nenad",
                   PrezimePredsednika = "Jecković",
                   EmailPredsednika = "jecko@mail.com"
                }
            });
        }

        public List<PredsednikModel> GetPredsednici(string ImePredsednika = null, string PrezimePredsednika = null, string EmailPredsednika = null)
        {

            return (from e in Predsednici
                    where string.IsNullOrEmpty(ImePredsednika) || e.ImePredsednika == ImePredsednika &&
                          string.IsNullOrEmpty(PrezimePredsednika) || e.PrezimePredsednika == PrezimePredsednika &&
                          string.IsNullOrEmpty(EmailPredsednika) || e.EmailPredsednika == EmailPredsednika
                    select e).ToList();
        }

        public PredsednikModel GetPredsednikById(Guid PredsednikId)
        {

            return Predsednici.FirstOrDefault(e => e.PredsednikId == PredsednikId);

        }

        public PredsednikConfirmation CreatePredsednik(PredsednikModel predsednik)
        {
            predsednik.PredsednikId = Guid.NewGuid();
            Predsednici.Add(predsednik);
            PredsednikModel pred = GetPredsednikById(predsednik.PredsednikId);

            return new PredsednikConfirmation
            {
                PredsednikId = pred.PredsednikId,

                ImePredsednika = pred.ImePredsednika,

                PrezimePredsednika = pred.PrezimePredsednika,

                EmailPredsednika = pred.EmailPredsednika,

            };
        }

        public PredsednikConfirmation UpdatePredsednik(PredsednikModel predsednik)
        {
            PredsednikModel pred = GetPredsednikById(predsednik.PredsednikId);

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
