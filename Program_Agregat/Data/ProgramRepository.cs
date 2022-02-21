using System;
using Program_Agregat.Models;
using Program_Agregat.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Data
{

    public class ProgramRepository : IProgramRepository
    {
        public static List<Program> Programi { get; set; } = new List<Program>();

        public ProgramRepository()
        {
            FillData();
        }

        private void FillData()
        {

        }

        public List<Program> GetProgrami()
        {

            return (from e in Programi                  
                    select e).ToList();
        }

        public Program GetProgramById(Guid ProgramId)
        {

            return Programi.FirstOrDefault(e => e.ProgramId == ProgramId);

        }

        public ProgramConfirmation CreateProgram(Program program)
        {
            program.ProgramId = Guid.NewGuid();
            Programi.Add(program);
            Program prog = GetProgramById(program.ProgramId);

            return new ProgramConfirmation
            {
                ProgramId = prog.ProgramId,

                MaksimalnoOgranicenje = prog.MaksimalnoOgranicenje

            };
        }

        public ProgramConfirmation UpdateProgram(Program program)
        {
            Program prog = GetProgramById(program.ProgramId);

            prog.ProgramId = program.ProgramId;
            prog.MaksimalnoOgranicenje = program.MaksimalnoOgranicenje;
            prog.Licitacije = program.Licitacije;
            

            return new ProgramConfirmation
            {
                ProgramId = program.ProgramId,
                MaksimalnoOgranicenje = program.MaksimalnoOgranicenje

            };
        }

        public void DeleteProgram(Guid ProgramId)
        {
            Programi.Remove(Programi.FirstOrDefault(e => e.ProgramId == ProgramId));
        }

    }
}