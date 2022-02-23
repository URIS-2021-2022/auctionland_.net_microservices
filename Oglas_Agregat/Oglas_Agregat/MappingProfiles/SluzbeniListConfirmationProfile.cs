using AutoMapper;
using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.MappingProfiles
{
    public class SluzbeniListConfirmationProfile : Profile
    {
        public SluzbeniListConfirmationProfile()
        {
            CreateMap<SluzbeniListConfirmation, SluzbeniListConfirmationDto>();

            CreateMap<SluzbeniList, SluzbeniListConfirmation>();
            //nzm da li sme 

            CreateMap<SluzbeniList, SluzbeniListConfirmationDto>();
            //ni ovoo

        }
    }
}
