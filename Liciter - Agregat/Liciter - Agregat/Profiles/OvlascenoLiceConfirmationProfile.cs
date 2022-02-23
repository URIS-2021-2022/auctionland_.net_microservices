using AutoMapper;
using Liciter___Agregat.DTOs.OvlascenoLice;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class OvlascenoLiceConfirmationProfile : Profile
    {
        public OvlascenoLiceConfirmationProfile()
        {
            CreateMap<OvlascenoLiceConfirmation, OvlascenoLiceConfirmationDto>();
            CreateMap<OvlascenoLiceModel, OvlascenoLiceConfirmation>();
        }
    }
}
