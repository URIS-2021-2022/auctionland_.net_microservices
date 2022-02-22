using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;
using Program_Agregat.Entities;

namespace Program_Agregat.Data
{
    public interface IPredlogPlanaRepository
    {
        List<PredlogPlana> GetPredloziPlana(string BrojDokumenta = null);

        PredlogPlana GetPredlogPlanaById(Guid PredlogPlanaId);

        PredlogPlanaConfirmation CreatePredlogPlana(PredlogPlana predlogPlana);

        PredlogPlanaConfirmation UpdatePredlogPlana(PredlogPlana predlogPlana);

        void DeletePredlogPlana(Guid PredlogPlanaId);

        bool SaveChanges();
    }
}