using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public class UplataZakupnineRepository : IUplataZakupnineRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public UplataZakupnineRepository (DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
        public UplataZakupnineConfirmation CreateUplataZakupnine(UplataZakupnine uplataZakupnine)
        {
        var createdEntity = context.Add(uplataZakupnine);
        return mapper.Map<UplataZakupnineConfirmation>(createdEntity.Entity);

    }

        public void DeleteUplataZakupnine(Guid UplataZakupnineId)
        {
            var uplata = GetUplataZakupnineById(UplataZakupnineId);
            context.Remove(uplata);
        }

        public UplataZakupnine GetUplataZakupnineById(Guid UplataZakupnineId)
        {
            return context.UplataZakupnine.Include(g => g.ugovorOZakupu).FirstOrDefault(e => e.UplataZakupnineID == UplataZakupnineId);
        }

        public List<UplataZakupnine> GetUplateZakupnine(string broj_racuna = null)
        {
            return context.UplataZakupnine.Include(g => g.ugovorOZakupu).Where(e => broj_racuna == null || broj_racuna == e.broj_racuna).ToList();
        }

        public UplataZakupnineConfirmation UpdateUplataZakupnine(UplataZakupnine uplataZakupnine)
        {
            UplataZakupnine zakupnina = GetUplataZakupnineById(uplataZakupnine.UplataZakupnineID);

            zakupnina.UplataZakupnineID = uplataZakupnine.UplataZakupnineID;
            zakupnina.broj_racuna = uplataZakupnine.broj_racuna;
            zakupnina.datum = uplataZakupnine.datum;
            zakupnina.iznos = uplataZakupnine.iznos;
            zakupnina.poziv_na_broj = uplataZakupnine.poziv_na_broj;
            zakupnina.svrha_uplate = uplataZakupnine.svrha_uplate;
            zakupnina.javno_nadmetanje = uplataZakupnine.javno_nadmetanje;
            zakupnina.uplatilac = uplataZakupnine.uplatilac;
            zakupnina.UgovorOZakupuID = uplataZakupnine.UgovorOZakupuID;
            zakupnina.ugovorOZakupu = uplataZakupnine.ugovorOZakupu;

            return new UplataZakupnineConfirmation
            {
                UplataZakupnineID = zakupnina.UplataZakupnineID,
                broj_racuna = zakupnina.broj_racuna,
                datum = zakupnina.datum
            };
        }
    }
    

    
}
