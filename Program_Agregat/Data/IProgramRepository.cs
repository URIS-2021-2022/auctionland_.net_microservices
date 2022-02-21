using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Program_Agregat.Models;
using Program_Agregat.Entities;

namespace Program_Agregat.Data
{
    public interface IProgramRepository
    {
        List<Program> GetProgrami();

        Program GetProgramById(Guid ProgramId);

        ProgramConfirmation CreateProgram(Program program);

        ProgramConfirmation UpdateProgram(Program program);

        void DeleteProgram(Guid ProgramId);
    }
}