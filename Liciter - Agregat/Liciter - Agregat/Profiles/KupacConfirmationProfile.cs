using AutoMapper;
using Liciter___Agregat.DTOs.Kupac;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class KupacConfirmationProfile : Profile
    {
        public KupacConfirmationProfile()
        {
            CreateMap<KupacConfirmation, KupacConfirmationDto>();
            CreateMap<KupacModel, KupacConfirmation>();
        }
    }
}
