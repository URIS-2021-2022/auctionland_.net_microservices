using AutoMapper;
using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.MappingProfiles
{
    public class OglasConfirmationProfile : Profile
    {
        public OglasConfirmationProfile()
        {
            CreateMap<OglasConfirmation, OglasConfirmationDto>();

            CreateMap<Oglas, OglasConfirmation>();
            //nzm da li sme

            CreateMap<Oglas, OglasConfirmationDto>();
            //ni ovo


        }
    }
}
