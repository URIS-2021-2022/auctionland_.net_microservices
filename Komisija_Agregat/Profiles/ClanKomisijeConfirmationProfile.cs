﻿using AutoMapper;
using Komisija_Agregat.Models;
using Komisija_Agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komisija_Agregat.Profiles
{
    public class ClanKomisijeConfirmationProfile : Profile
    {
        public ClanKomisijeConfirmationProfile()
        {
            CreateMap<ClanKomisijeConfirmation, ClanKomisijeConfirmationDto>();
            CreateMap<ClanKomisije, ClanKomisijeConfirmation>();

        }
    }
}