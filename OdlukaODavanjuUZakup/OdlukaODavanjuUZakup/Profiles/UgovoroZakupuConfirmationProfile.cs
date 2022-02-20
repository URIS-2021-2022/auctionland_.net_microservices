using AutoMapper;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Profiles
{
    public class UgovoroZakupuConfirmationProfile : Profile
    {
        public UgovoroZakupuConfirmationProfile()
        {
            CreateMap<UgovoroZakupuConfirmation, UgovoroZakupuConfirmationDto>();
        }
    }
}
