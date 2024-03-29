﻿using AutoMapper;
using Program_Agregat.Entities;
using Program_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program_Agregat.Profiles
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<ProgramEntity, ProgramDto>();
            CreateMap<ProgramCreationDto, ProgramEntity>();
            CreateMap<ProgramUpdateDto, ProgramEntity>();
        }
    }
}