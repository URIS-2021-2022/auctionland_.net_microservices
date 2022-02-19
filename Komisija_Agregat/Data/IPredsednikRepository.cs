using System;
using Komisija_Agregat.Models;

namespace Komisija_Agregat.Data
{
    public interface IPredsednikRepository
    {
        List<PredsednikModel> GetPredsednici(string ImePredsednika = null, string PrezimePredsednika = null, string EmailPredsednika = null);

        PredsednikModel GetPredsednikById(Guid PredsednikId);

        PredsednikConfirmation CreatePredsednik(PredsednikModel predsednik);

        PredsednikConfirmation UpdatePredsednik(PredsednikModel predsednik);

        void DeletePredsednik(Guid PredsednikId);
    }
}
