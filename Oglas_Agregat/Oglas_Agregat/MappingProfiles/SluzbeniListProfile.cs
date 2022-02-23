using AutoMapper;
using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.MappingProfiles
{
    public class SluzbeniListProfile : Profile
    {
        public SluzbeniListProfile()
        {
            CreateMap<SluzbeniList, SluzbeniListDto>();

            CreateMap<SluzbeniListCreateDto, SluzbeniList>();

            CreateMap<SluzbeniListUpdateDto, SluzbeniList>();

            CreateMap<SluzbeniListDto, SluzbeniList>();

            CreateMap<SluzbeniList, SluzbeniList>();
        }
    }
}
