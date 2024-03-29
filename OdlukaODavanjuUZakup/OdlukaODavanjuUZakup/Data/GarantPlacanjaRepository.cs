﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{

    public class GarantPlacanjaRepository : IGarantPlacanjaRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;


        public GarantPlacanjaRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public GarantPlacanjaConfirmation CreateGarantPlacanja(GarantPlacanja garantPlacanja)
        {
             
            var createdEntity = context.Add(garantPlacanja);
            return mapper.Map<GarantPlacanjaConfirmation>(createdEntity.Entity);
        }

        public void DeleteGarantPlacanja(Guid GarantPlacanjaId)
        {
            var garantPlacanja = GetGarantPlacanjaById(GarantPlacanjaId);
            context.Remove(garantPlacanja);
        }

        public List<GarantPlacanja> GetGarantiPlacanja()
        {
            return context.GarantPlacanja.Include(g => g.ugovorOZakupu).ToList();
        }

        public GarantPlacanja GetGarantPlacanjaById(Guid GarantPlacanjaId)
        {
            return context.GarantPlacanja.Include(g=>g.ugovorOZakupu).FirstOrDefault(e => e.GarantPlacanjaID == GarantPlacanjaId);
        }

        public GarantPlacanjaConfirmation UpdateGarantPlacanja(GarantPlacanja garantPlacanja)
        {
            GarantPlacanja garant = GetGarantPlacanjaById(garantPlacanja.GarantPlacanjaID);

            garant.GarantPlacanjaID = garantPlacanja.GarantPlacanjaID;
            garant.Opis_garanta1 = garantPlacanja.Opis_garanta1;
            garant.Opis_garanta2 = garantPlacanja.Opis_garanta2;
            garant.ugovorOZakupu = garantPlacanja.ugovorOZakupu;
            garant.UgovorOZakupuID = garantPlacanja.UgovorOZakupuID;

            return new GarantPlacanjaConfirmation
            {
                GarantPlacanjaID = garant.GarantPlacanjaID,
                Opis_garanta1 = garant.Opis_garanta1

            };
        }
    }
}
