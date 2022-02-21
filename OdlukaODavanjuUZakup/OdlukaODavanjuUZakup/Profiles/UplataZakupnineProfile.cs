using AutoMapper;
using OdlukaODavanjuUZakup.Entities;
using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Profiles
{
    public class UplataZakupnineProfile : Profile
    {
        public UplataZakupnineProfile()
        {
            CreateMap<UplataZakupnine, UplataZakupnineDto>();

            CreateMap<UplataZakupnineCreationDto, UplataZakupnine>();

            CreateMap<UplataZakupnineUpdateDto, UplataZakupnine>();
        }
    }
}
