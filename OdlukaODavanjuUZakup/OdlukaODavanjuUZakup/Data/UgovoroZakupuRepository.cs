using AutoMapper;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OdlukaODavanjuUZakup.Data
{
    public class UgovoroZakupuRepository : IUgovoroZakupuRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;
        //    public static List<UgovoroZakupu> UgovorioZakupu { get; set; } = new List<UgovoroZakupu>();

        public UgovoroZakupuRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public UgovoroZakupuConfirmation CreateUgovorOZakupu(UgovoroZakupu ugovoroZakupu)
        {
            /*  ugovoroZakupu.UgovoroZakupuID = Guid.NewGuid();
              UgovorioZakupu.Add(ugovoroZakupu);
              UgovoroZakupu ugovor = GetUgovoriOZakupuById(ugovoroZakupu.UgovoroZakupuID);

              return new UgovoroZakupuConfirmation
              {
                  UgovoroZakupuID = ugovor.UgovoroZakupuID,
                  datum_potpisa = ugovor.datum_potpisa,
                  zavodni_Broj = ugovor.zavodni_Broj
              }; */
            var createdEntity = context.Add(ugovoroZakupu);
            return mapper.Map<UgovoroZakupuConfirmation>(createdEntity.Entity);

        }

        public void DeleteUgovorOZakupu(Guid UgovoroZakupuId)
        {
            var ugovor = GetUgovoriOZakupuById(UgovoroZakupuId);
            context.Remove(ugovor);
            //    context.Remove(UgovoroZakupu.FirstOrDefault(e => e.UgovoroZakupuID == UgovoroZakupuId));
        }

        public List<UgovoroZakupu> GetUgovoriOZakupu(string zavodni_broj = null)
        {
            return context.UgovoroZakupu.Include(g => g.odlukaoDavanjuuZakup).Where(e => zavodni_broj == null || e.zavodni_Broj == zavodni_broj).ToList();
        }

        public UgovoroZakupu GetUgovoriOZakupuById(Guid UgovoroZakupuId)
        {
            return context.UgovoroZakupu.Include(g => g.odlukaoDavanjuuZakup).FirstOrDefault(e => e.UgovoroZakupuID == UgovoroZakupuId);
        }

        public UgovoroZakupuConfirmation UpdateUgovorOZakupu(UgovoroZakupu ugovoroZakupu)
        {
            UgovoroZakupu ugovor = GetUgovoriOZakupuById(ugovoroZakupu.UgovoroZakupuID);

            ugovor.UgovoroZakupuID = ugovoroZakupu.UgovoroZakupuID;
            ugovor.mesto_potpisivanja = ugovoroZakupu.mesto_potpisivanja;
            ugovor.datum_potpisa = ugovoroZakupu.datum_potpisa;
            ugovor.datum_zavodjenja = ugovoroZakupu.datum_zavodjenja;
            ugovor.rokovi_dospeca = ugovoroZakupu.rokovi_dospeca;
            ugovor.rok_za_vracanje_zemljista = ugovoroZakupu.rok_za_vracanje_zemljista;
            ugovor.tip_garancije = ugovoroZakupu.tip_garancije;
            ugovor.zavodni_Broj = ugovoroZakupu.zavodni_Broj;
            ugovor.Javno_Nadmetanje = ugovoroZakupu.Javno_Nadmetanje;
            ugovor.lice = ugovoroZakupu.lice;
            ugovor.odluka = ugovoroZakupu.odluka;

            return new UgovoroZakupuConfirmation
            {
                UgovoroZakupuID = ugovor.UgovoroZakupuID,
                datum_potpisa = ugovor.datum_potpisa,
                zavodni_Broj = ugovor.zavodni_Broj
            };
        }

    }
}
