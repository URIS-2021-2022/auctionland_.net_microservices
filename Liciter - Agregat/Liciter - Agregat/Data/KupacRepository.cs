using AutoMapper;
using Liciter___Agregat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class KupacRepository : IKupacRepository
    {
        private readonly DataBaseContext context;
        private readonly IMapper mapper;

        public KupacRepository(DataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public KupacConfirmation CreateKupac(KupacModel kupac)
        {
            var createdEntity = context.Add(kupac);
            return mapper.Map<KupacConfirmation>(createdEntity.Entity);
        }

        public void DeleteKupac(Guid KupacId)
        {
            var kupac = GetKupacById(KupacId);
            context.Remove(kupac);
        }

        public KupacModel GetKupacById(Guid KupacId)
        {
            return context.Kupci.Include(f => f.FizickoLice).Include(p => p.PravnoLice).Include(o=>o.OvlascenaLica).FirstOrDefault(e => e.KupacId == KupacId);
        }

        public List<KupacModel> GetKupci(string JMBG_MaticniBroj = null)
        {
         
            return context.Kupci.Include(f => f.FizickoLice).Include(p => p.PravnoLice).Include(o=>o.OvlascenaLica).Where(e => (JMBG_MaticniBroj == null))
                .ToList();
        }

        public KupacConfirmation UpdateKupac(KupacModel kupac)
        {
            KupacModel kupac2 = GetKupacById(kupac.KupacId);

            kupac2.KupacId = kupac.KupacId;
            kupac2.ImaZabranu = kupac.ImaZabranu;
            kupac2.DatumPocetkaZabrane = kupac.DatumPocetkaZabrane;
            kupac2.DatumPrestankaZabrane = kupac.DatumPrestankaZabrane;
            kupac2.DuzinaTrajanjaZabraneGod = kupac.DuzinaTrajanjaZabraneGod;
            kupac2.FizickoLice = kupac.FizickoLice;
            kupac2.JavnaNadmetanjaId = kupac.JavnaNadmetanjaId;
            kupac2.OstvarenaPovrsina = kupac.OstvarenaPovrsina;
            kupac2.OvlascenaLica = kupac.OvlascenaLica;
            kupac2.PravnoLice = kupac.PravnoLice;
            kupac2.Prioritet = kupac.Prioritet;
            kupac2.JavnoNadmetanjeId = kupac.JavnoNadmetanjeId;
            

            
                return new KupacConfirmation
                {
                    Prioritet = kupac2.Prioritet,
                    OstvarenaPovrsina = kupac2.OstvarenaPovrsina,
                    ImaZabranu = kupac2.ImaZabranu

                };
          
            }
        }
    }

