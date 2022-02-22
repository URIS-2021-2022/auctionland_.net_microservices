using AutoMapper;
using Korisnik_agregat.Entities;
using Korisnik_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Profiles
{
    public class KorisnikConfirmationProfile : Profile
    {
        
        public KorisnikConfirmationProfile()
        {
            CreateMap<Korisnik, KorisnikConfirmationDto>();
        }
        
    }
}
