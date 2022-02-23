using AutoMapper;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public class OdlukaoDavanjuuZakupRepository :IOdlukaoDavanjuuZakupRepository
    {


        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public OdlukaoDavanjuuZakupRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public List<OdlukaoDavanjuuZakup> GetOdluke()
        {
            return context.OdlukaoDavanjuuZakup.ToList();
        }

        public OdlukaoDavanjuuZakup GetOdlukaById(Guid OdlukaoDavanjuuZakupId)
        {
            return context.OdlukaoDavanjuuZakup.FirstOrDefault(e => e.OdlukaoDavanjuuZakupID == OdlukaoDavanjuuZakupId);
        }

        public OdlukaoDavanjuuZakupConfirmation CreateOdluka(OdlukaoDavanjuuZakup OdlukaoDavanjuuZakup)
        {

            var createdEntity = context.Add(OdlukaoDavanjuuZakup);
            return mapper.Map<OdlukaoDavanjuuZakupConfirmation>(createdEntity.Entity);
        }

        public OdlukaoDavanjuuZakupConfirmation UpdateOdluka(OdlukaoDavanjuuZakup OdlukaoDavanjuuZakup)
        {
            OdlukaoDavanjuuZakup odluka = GetOdlukaById(OdlukaoDavanjuuZakup.OdlukaoDavanjuuZakupID);

            odluka.OdlukaoDavanjuuZakupID = OdlukaoDavanjuuZakup.OdlukaoDavanjuuZakupID;
            odluka.datum_donosenja_odluke = OdlukaoDavanjuuZakup.datum_donosenja_odluke;
            odluka.validnost = OdlukaoDavanjuuZakup.validnost;
            return new OdlukaoDavanjuuZakupConfirmation
            {
                OdlukaoDavanjuuZakupID = odluka.OdlukaoDavanjuuZakupID,
                validnost = odluka.validnost
            };
        }

        public void DeleteOdluka(Guid OdlukaoDavanjuuZakupId)
        {
            var odluka = GetOdlukaById(OdlukaoDavanjuuZakupId);
            context.Remove(odluka);
        }
    }
}
