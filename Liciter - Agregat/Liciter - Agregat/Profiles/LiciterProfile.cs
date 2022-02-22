using AutoMapper;
using Liciter___Agregat.DTOs.Liciter;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class LiciterProfile : Profile
    {
        public LiciterProfile()
        {
            CreateMap<LiciterModel, LiciterDto>();
            CreateMap<LiciterCreationDto, LiciterModel>();
            CreateMap<LiciterUpdateDto,LiciterModel>();
        }
    }
}
