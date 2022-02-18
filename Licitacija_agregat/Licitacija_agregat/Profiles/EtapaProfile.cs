using AutoMapper;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Profiles
{
    public class EtapaProfile : Profile
    {
        public EtapaProfile()
        {
            CreateMap<Etapa, EtapaDto>(); //shallow copy

            CreateMap<EtapaCreationDto, Etapa>();

            CreateMap<EtapaUpdateDto, Etapa>();
        }
    }
}
