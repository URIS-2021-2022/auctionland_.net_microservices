using System;
using Program_Agregat.Models;

namespace Program_Agregat.Data
{
    public interface IPredlogPlanaRepository
    {
        List<PredlogPlanaModel> GetPredloziPlana(int BrojDokumenta = null);

        PredlogPlanaModel GetPredlogPlanaById(Guid PredlogPlanaId);

        PredlogPlanaConfirmation CreatePredlogPlana(PredlogPlanaModel predlogPlana);

        PredlogPlanaConfirmation UpdatePredlogPlana(PredlogPlanaModel predlogPlana);

        void DeletePredlogPlana(Guid PredlogPlanaId);
    }
}