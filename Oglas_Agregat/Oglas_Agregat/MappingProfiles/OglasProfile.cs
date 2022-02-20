using AutoMapper;
using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.MappingProfiles
{
    public class OglasProfile : Profile
    {
        public OglasProfile()
        {
            CreateMap<Oglas, OglasDto>(); //shallow copy, ako bi prvo imalo nesto unutar sebe te reference se ne kopiraju nego samo vrednosti
            
            CreateMap<OglasCreateDto, Oglas>();
            CreateMap<OglasUpdateDto, Oglas>();
            CreateMap<Oglas, OglasConfirmation>();

            //mozda ne sme 
            CreateMap<Oglas, OglasCreateDto>();
            CreateMap<Oglas, OglasUpdateDto>();
        }
    }
}
