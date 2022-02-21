using AutoMapper;
using Korisnik_agregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Profiles
{
    public class KorisnikConfirmationDto : Profile
    {
        public KorisnikConfirmationDto()
        {
            CreateMap<Korisnik, KorisnikConfirmationDto>();

            CreateMap<KorisnikConfirmationDto, KorisnikConfirmationDto>();
        }
    }
}
