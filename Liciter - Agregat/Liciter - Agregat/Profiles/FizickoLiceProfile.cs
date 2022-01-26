using AutoMapper;
using Liciter___Agregat.DTOs.FizickoLice;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class FizickoLiceProfile : Profile
    {
        public FizickoLiceProfile()
        {
            CreateMap<FizickoLiceModel, FizickoLiceDto>()
                .ForMember(
                dest => dest.Ime_Prezime,
                opt => opt.MapFrom(src => src.Ime + " " + src.Prezime))
                .ForMember(
                dest => dest.BrojeviTelefona,
                opt => opt.MapFrom(src => src.BrojTelefona1 + " , " + src.BrojTelefona2));
            CreateMap<FizickoLiceModel, FizickoLiceUpdateDto>();
            CreateMap<FizickoLiceModel, FizickoLiceCreationDto>();
        }
    }
}
