using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public interface IUplataZakupnineRepository
    {
        List<UplataZakupnine> GetUplateZakupnine(string broj_racuna = null);

        UplataZakupnine GetUplataZakupnineById(Guid UplataZakupnineId);

        UplataZakupnineConfirmationDto CreateUplataZakupnine(UplataZakupnine uplataZakupnine);

        UplataZakupnineConfirmationDto UpdateUplataZakupnine(UplataZakupnine uplataZakupnine);

        void DeleteUplataZakupnine(Guid UplataZakupnineId);
    }
}
