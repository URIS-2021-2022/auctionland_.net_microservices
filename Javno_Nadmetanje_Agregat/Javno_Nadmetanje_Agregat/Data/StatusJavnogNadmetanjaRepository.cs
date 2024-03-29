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
    public class StatusJavnogNadmetanjaRepository : IStatusJavnogNadmetanjaRepository
    {
        private readonly JavnoNadmetanjeContext Context;
        private readonly IMapper Mapper;

        public StatusJavnogNadmetanjaRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public StatusJavnogNadmetanjaConfirmationDto CreateStatusJavnogNadmetanja(StatusJavnogNadmetanja statusJavnogNadmetanja)
        {
            statusJavnogNadmetanja.StatusJavnogNadmetanjaId = Guid.NewGuid();

            Context.StatusJavnogNadmetanja.Add(statusJavnogNadmetanja);
            Context.SaveChanges();

            return Mapper.Map<StatusJavnogNadmetanjaConfirmationDto>(statusJavnogNadmetanja);
        }

        public StatusJavnogNadmetanjaConfirmationDto DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaId)
        {
            StatusJavnogNadmetanja statusJavnogNadmetanja = GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaId);

            if (statusJavnogNadmetanja == null)
            {
                throw new ArgumentNullException(nameof(statusJavnogNadmetanjaId));
            }

            Context.StatusJavnogNadmetanja.Remove(statusJavnogNadmetanja);
            Context.SaveChanges();

            return Mapper.Map<StatusJavnogNadmetanjaConfirmationDto>(statusJavnogNadmetanja);
        }

        public StatusJavnogNadmetanja GetStatusJavnogNadmetanjaById(Guid statusJavnogNadmetanjaId)
        {
            return Context.StatusJavnogNadmetanja.FirstOrDefault(e => e.StatusJavnogNadmetanjaId == statusJavnogNadmetanjaId);
        }

        public List<StatusJavnogNadmetanja> GetStatusJavnogNadmetanjaList()
        {
            return Context.StatusJavnogNadmetanja.Include(d => d.ListaJavnihNadmetanja).ToList();
        }

        public StatusJavnogNadmetanjaConfirmationDto UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanja statusJavnogNadmetanja)
        {
            StatusJavnogNadmetanja sjn = Context.StatusJavnogNadmetanja.FirstOrDefault(e => e.StatusJavnogNadmetanjaId == statusJavnogNadmetanja.StatusJavnogNadmetanjaId);

            if (sjn == null)
            {
                throw new EntryPointNotFoundException();
            }

            sjn.StatusJavnogNadmetanjaId = statusJavnogNadmetanja.StatusJavnogNadmetanjaId;
            sjn.NazivStatusaJavnogNadmetanja = statusJavnogNadmetanja.NazivStatusaJavnogNadmetanja;

            Context.SaveChanges();

            return Mapper.Map<StatusJavnogNadmetanjaConfirmationDto>(sjn);
        }
    }
}
