using System;
using Program_Agregat.Models;

namespace Program_Agregat.Data
{

    public class ProgramRepository : IProgramRepository
    {
        public static List<ProgramModel> Programi { get; set; } = new List<ProgramModel>();

        public ProgramRepository()
        {
            FillData();
        }

        private void FillData()
        {

        }

        public List<ProgramModel> GetProgrami(string MaksimalnoOgranicenje = null)
        {

            return (from e in Programi
                    string.IsNullOrEmpty(MaksimalnoOgranicenje) || e.MaksimalnoOgranicenje == MaksimalnoOgranicenje
                    select e).ToList();
        }

        public ProgramModel GetProgramById(Guid ProgramId)
        {

            return Programi.FirstOrDefault(e => e.ProgramId == ProgramId);

        }

        public ProgramConfirmation CreateProgram(ProgramModel program)
        {
            program.ProgramId = Guid.NewGuid();
            Programi.Add(program);
            ProgramModel prog = GetProgramById(program.ProgramId);

            return new ProgramConfirmation
            {
                ProgramId = prog.ProgramId,

                MaksimalnoOgranicenje = prog.MaksimalnoOgranicenje

            };
        }

        public ProgramConfirmation UpdateProgram(ProgramModel program)
        {
            ProgramModel prog = GetProgramById(program.ProgramId);

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