using AutoMapper;
using Liciter___Agregat.DTOs.FizickoLice;
using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Profiles
{
    public class FizickoLiceConfirmationProfile : Profile
    {
        public FizickoLiceConfirmationProfile()
        {
            CreateMap<FizickoLiceConfirmation, FizickoLiceConfirmationDto>();
            CreateMap<FizickoLiceModel, FizickoLiceConfirmation>();

        }
    }
}
