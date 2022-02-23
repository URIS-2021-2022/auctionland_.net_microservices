using System;
using Program_Agregat.Models;
using Program_Agregat.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Program_Agregat.Data
{

    public class ProgramRepository : IProgramRepository
    {
        private readonly ProgramContext context;
        private readonly IMapper mapper;

        public ProgramRepository(ProgramContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<ProgramEntity> GetProgrami()
        {

            return context.Programi.ToList();
        }

        public ProgramEntity GetProgramById(Guid ProgramId)
        {

            return context.Programi.FirstOrDefault(e => e.ProgramId == ProgramId);

        }

        public ProgramConfirmation CreateProgram(ProgramEntity program)
        {
            var createdEntity = context.Add(program);
            return mapper.Map<ProgramConfirmation>(createdEntity.Entity);
        }

        public ProgramConfirmation UpdateProgram(ProgramEntity program)
        {
            ProgramEntity prog = GetProgramById(program.ProgramId);

            prog.ProgramId = program.ProgramId;
            prog.MaksimalnoOgranicenje = program.MaksimalnoOgranicenje;
            prog.Licitacije = program.Licitacije;


            return new ProgramConfirmation
            {
                ProgramId = program.ProgramId
            };
        }

        public void DeleteProgram(Guid ProgramId)
        {
            context.Remove(GetProgramById(ProgramId));
        }

    }
}