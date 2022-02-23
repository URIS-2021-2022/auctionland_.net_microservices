using AutoMapper;
using Liciter___Agregat.DTOs.OvlascenoLice;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class OvlascenoLiceProfile : Profile
    {
        public OvlascenoLiceProfile()
        {
            CreateMap<OvlascenoLiceModel, OvlascenoLiceDto>()
                .ForMember(
                dest => dest.Ime_Prezime,
                opt => opt.MapFrom(src => src.Ime + " " + src.Prezime));
            CreateMap<OvlascenoLiceUpdateDto, OvlascenoLiceModel>();
            CreateMap<OvlascenoLiceCreationDto, OvlascenoLiceModel>();
        }
    }
}
