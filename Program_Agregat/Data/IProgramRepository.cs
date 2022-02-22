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
        List<ProgramEntity> GetProgrami();

        ProgramEntity GetProgramById(Guid ProgramId);

        ProgramConfirmation CreateProgram(ProgramEntity program);

        ProgramConfirmation UpdateProgram(ProgramEntity program);

        void DeleteProgram(Guid ProgramId);

        bool SaveChanges();
    }
}