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
  //      public static List<UplataZakupnine> UplateZakupnine { get; set; } = new List<UplataZakupnine>();

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
        /*     uplataZakupnine.UplataZakupnineID = Guid.NewGuid();
             UplateZakupnine.Add(uplataZakupnine); 
             UplataZakupnine zakupnina = GetUplataZakupnineById(uplataZakupnine.UplataZakupnineID);

             return new UplataZakupnineConfirmation
             {
                 UplataZakupnineID = zakupnina.UplataZakupnineID,
                 broj_racuna = zakupnina.broj_racuna,
                 datum = zakupnina.datum
             }; */
        var createdEntity = context.Add(uplataZakupnine);
        return mapper.Map<UplataZakupnineConfirmation>(createdEntity.Entity);

    }

        public void DeleteUplataZakupnine(Guid uplataZakupnineId)
        {
            var uplata = GetUplataZakupnineById(uplataZakupnineId);
            context.Remove(uplata);
       //     context.Remove(UplataZakupnine.FirstOrDefault(e => e.UplataZakupnineID == UplataZakupnineId));
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
