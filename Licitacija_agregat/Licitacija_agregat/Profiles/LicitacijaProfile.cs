using AutoMapper;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Profiles
{
    public class LicitacijaProfile : Profile
    {
        public LicitacijaProfile()
        {
            CreateMap<Licitacija, LicitacijaDto>();

            CreateMap<LicitacijaCreationDto, Licitacija>();

            CreateMap<LicitacijaUpdateDto, Licitacija>();
        }
        
    }
}
