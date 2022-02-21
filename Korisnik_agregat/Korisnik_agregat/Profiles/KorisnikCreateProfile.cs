using AutoMapper;
using Korisnik_agregat.Entities;
using Korisnik_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Profiles
{
    public class KorisnikCreateProfile : Profile
    {
        public KorisnikCreateProfile()
        {
            CreateMap<KorisnikCreationDto, Korisnik>();
        }
    }
}
