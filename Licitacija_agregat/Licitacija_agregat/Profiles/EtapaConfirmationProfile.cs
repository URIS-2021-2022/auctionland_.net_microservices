using AutoMapper;
using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Profiles
{
    public class EtapaConfirmationProfile : Profile
    {
        public EtapaConfirmationProfile()
        {
            CreateMap<EtapaConfirmation, EtapaConfirmationDto>();

            CreateMap<Etapa, EtapaConfirmation>();
        }
    }
}
