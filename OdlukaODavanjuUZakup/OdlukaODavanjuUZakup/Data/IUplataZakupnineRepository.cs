using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    interface IUplataZakupnineRepository
    {
        List<UplataZakupnineModel> GetUplateZakupnine(string broj_racuna = null);

        UplataZakupnineModel GetUplataZakupnineById(Guid UplataZakupnineId);

        UplataZakupnineConfirmation CreateUplataZakupnine(UplataZakupnineModel uplataZakupnine);

        UplataZakupnineConfirmation UpdateUplataZakupnine(UplataZakupnineModel uplataZakupnine);

        void DeleteUplataZakupnine(Guid UplataZakupnineId);
    }
}
