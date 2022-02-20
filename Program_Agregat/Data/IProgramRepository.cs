using System;
using Program_Agregat.Models;

namespace Program_Agregat.Data
{
    public interface IProgramRepository
    {
        List<ProgramModel> GetProgrami(string MaksimalnoOgranicenje = null);

        ProgramModel GetProgramById(Guid ProgramId);

        ProgramConfirmation CreateProgram(ProgramModel program);

        ProgramConfirmation UpdateProgram(ProgramModel program);

        void DeleteProgram(Guid ProgramId);
    }
}