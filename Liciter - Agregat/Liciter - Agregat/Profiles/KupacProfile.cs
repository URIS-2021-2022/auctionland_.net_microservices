using AutoMapper;
using Liciter___Agregat.DTOs.Kupac;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class KupacProfile : Profile
    {
        public KupacProfile()
        {
            CreateMap<KupacModel, KupacDto>();
            CreateMap<KupacCreationDto, KupacModel >();
            CreateMap<KupacUpdateDto, KupacModel >();
        }
    }
}
