﻿using AutoMapper;
using Javno_Nadmetanje_Agregat.Entities;
using Javno_Nadmetanje_Agregat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.Data
{
    public class TipJavnogNadmetanjaRepository : ITipJavnogNadmetanjaRepository
    {
        private readonly JavnoNadmetanjeContext Context;
        private readonly IMapper Mapper;

        public TipJavnogNadmetanjaRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public TipJavnogNadmetanjaConfirmationDto CreateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja)
        {
            tipJavnogNadmetanja.TipJavnogNadmetanjaId = Guid.NewGuid();

            Context.TipJavnogNadmetanja.Add(tipJavnogNadmetanja);
            Context.SaveChanges();

            return Mapper.Map<TipJavnogNadmetanjaConfirmationDto>(tipJavnogNadmetanja);
        }

        public TipJavnogNadmetanjaConfirmationDto DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaId)
        {
            TipJavnogNadmetanja tipJavnogNadmetanja = GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaId);

            if (tipJavnogNadmetanja == null)
            {
                throw new ArgumentNullException(nameof(tipJavnogNadmetanjaId));
            }

            Context.TipJavnogNadmetanja.Remove(tipJavnogNadmetanja);
            Context.SaveChanges();

            return Mapper.Map<TipJavnogNadmetanjaConfirmationDto>(tipJavnogNadmetanja);
        }

        public TipJavnogNadmetanja GetTipJavnogNadmetanjaById(Guid tipJavnogNadmetanjaId)
        {
            return Context.TipJavnogNadmetanja.FirstOrDefault(e => e.TipJavnogNadmetanjaId == tipJavnogNadmetanjaId);
        }

        public List<TipJavnogNadmetanja> GetTipJavnogNadmetanjaList()
        {
            return Context.TipJavnogNadmetanja.Include(d => d.ListaJavnihNadmetanja).ToList();
        }

        public TipJavnogNadmetanjaConfirmationDto UpdateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja)
        {
            TipJavnogNadmetanja tjn = Context.TipJavnogNadmetanja.FirstOrDefault(e => e.TipJavnogNadmetanjaId == tipJavnogNadmetanja.TipJavnogNadmetanjaId);

            if (tjn == null)
            {
                throw new EntryPointNotFoundException();
            }

            tjn.TipJavnogNadmetanjaId = tipJavnogNadmetanja.TipJavnogNadmetanjaId;
            tjn.NazivTipaJavnogNadmetanja = tipJavnogNadmetanja.NazivTipaJavnogNadmetanja;

            Context.SaveChanges();

            return Mapper.Map<TipJavnogNadmetanjaConfirmationDto>(tjn);
        }
    }
}
