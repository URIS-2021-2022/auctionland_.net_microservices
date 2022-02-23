using AutoMapper;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Profiles
{
    public class LicitacijaConfirmationProfile : Profile
    {
        public LicitacijaConfirmationProfile()
        {
            CreateMap<LicitacijaConfirmation, LicitacijaConfirmationDto>();

            CreateMap<Licitacija, LicitacijaConfirmation>();
        }
    }
}
