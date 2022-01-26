using AutoMapper;
using Liciter___Agregat.DTOs.Liciter;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class LiciterConfirmationProfile : Profile
    {
        public LiciterConfirmationProfile()
        {
            CreateMap<LiciterConfirmation, LiciterConfirmationDto>();
        }
    }
}
