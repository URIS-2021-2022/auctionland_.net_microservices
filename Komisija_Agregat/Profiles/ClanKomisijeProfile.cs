using AutoMapper;
using Komisija_Agregat.Models;
using Komisija_Agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Profiles
{
    public class ClanKomisijeProfile : Profile
    {
        public ClanKomisijeProfile()
        {
            CreateMap<ClanKomisije, ClanKomisijeDto>();
            CreateMap<ClanKomisijeCreationDto, ClanKomisije>();
            CreateMap<ClanKomisijeUpdateDto, ClanKomisije>();
        }
    }
}