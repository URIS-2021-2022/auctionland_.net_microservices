using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;
using Program_Agregat.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Program_Agregat.Data
{

    public class PredlogPlanaRepository : IPredlogPlanaRepository
    {
        private readonly ProgramContext context;
        private readonly IMapper mapper;

        public PredlogPlanaRepository(ProgramContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<PredlogPlana> GetPredloziPlana(string BrojDokumenta = null)
        {

            return context.PredloziPlana.Include(p=>p.ProgramPlana).Where(e => string.IsNullOrEmpty(BrojDokumenta) || e.BrojDokumenta == BrojDokumenta).ToList();
        }

        public PredlogPlana GetPredlogPlanaById(Guid PredlogPlanaId)
        {

            return context.PredloziPlana.Include(p => p.ProgramPlana).FirstOrDefault(e => e.PredlogPlanaId == PredlogPlanaId);

        }

        public PredlogPlanaConfirmation CreatePredlogPlana(PredlogPlana predlogPlana)
        {
            var createdEntity = context.Add(predlogPlana);
            return mapper.Map<PredlogPlanaConfirmation>(createdEntity.Entity);
        }

        public PredlogPlanaConfirmation UpdatePredlogPlana(PredlogPlana predlogPlana)
        {
            PredlogPlana predlog = GetPredlogPlanaById(predlogPlana.PredlogPlanaId);

            predlog.PredlogPlanaId = predlogPlana.PredlogPlanaId;
            predlog.BrojDokumenta = predlogPlana.BrojDokumenta;
            predlog.MisljenjeKomisije = predlogPlana.MisljenjeKomisije;
            predlog.Usvojen = predlogPlana.Usvojen;
            predlog.DatumDokumenta = predlogPlana.DatumDokumenta;
            predlog.ProgramPlana = predlogPlana.ProgramPlana;
            predlog.ProgramId = predlogPlana.ProgramId;

            return new PredlogPlanaConfirmation
            {
                PredlogPlanaId = predlog.PredlogPlanaId,
                BrojDokumenta = predlog.BrojDokumenta

            };
        }

        public void DeletePredlogPlana(Guid PredlogPlanaId)
        {
            context.Remove(GetPredlogPlanaById(PredlogPlanaId));
        }

    }
}