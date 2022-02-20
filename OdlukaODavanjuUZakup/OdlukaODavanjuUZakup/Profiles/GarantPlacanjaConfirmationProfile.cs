using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace OdlukaODavanjuUZakup.Profiles
{
    public class GarantPlacanjaConfirmationProfile : Profile
    {
        public GarantPlacanjaConfirmationProfile()
        {
            CreateMap<GarantPlacanjaConfirmation, GarantPlacanjaConfirmationDto>();
        }
    }
}
