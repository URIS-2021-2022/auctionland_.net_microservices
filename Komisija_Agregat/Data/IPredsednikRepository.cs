using System;
using Komisija_Agregat.Models;
using Komisija_Agregat.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Data
{
    public interface IPredsednikRepository
    {
        List<Predsednik> GetPredsednici(string ImePredsednika = null, string PrezimePredsednika = null, string EmailPredsednika = null);

        Predsednik GetPredsednikById(Guid PredsednikId);

        PredsednikConfirmation CreatePredsednik(Predsednik predsednik);

        PredsednikConfirmation UpdatePredsednik(Predsednik predsednik);

        void DeletePredsednik(Guid PredsednikId);

        bool SaveChanges();

    }
}