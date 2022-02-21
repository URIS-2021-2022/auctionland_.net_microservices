﻿using AutoMapper;
using Javno_Nadmetanje_Agregat.Entities;
using Javno_Nadmetanje_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat.MappingProfiles
{
    public class JavnoNadmetanjeProfile : Profile
    {
        public JavnoNadmetanjeProfile()
        {
            CreateMap<JavnoNadmetanje, JavnoNadmetanjeDto>();
        }
    }
}