using AutoMapper;
using Liciter___Agregat.DTOs.PravnoLice;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class PravnoLiceProfile : Profile
    {
        public PravnoLiceProfile()
        {
            CreateMap<PravnoLiceModel, PravnoLiceDto>()
                .ForMember(
                dest => dest.BrojeviTelefona,
                opt => opt.MapFrom(src => src.BrojTelefona1 + " , " + src.BrojTelefona2));
            CreateMap<PravnoLiceModel, PravnoLiceCreationDto>();
            CreateMap<PravnoLiceModel, PravnoLiceUpdateDto>();
        }
    }
}
