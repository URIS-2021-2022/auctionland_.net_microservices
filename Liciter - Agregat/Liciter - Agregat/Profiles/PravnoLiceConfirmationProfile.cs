using AutoMapper;
using Liciter___Agregat.DTOs.PravnoLice;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class PravnoLiceConfirmationProfile : Profile
    {
        public PravnoLiceConfirmationProfile()
        {
            CreateMap<PravnoLiceConfirmation, PravnoLiceConfirmationDto>();
        }
    }
}
