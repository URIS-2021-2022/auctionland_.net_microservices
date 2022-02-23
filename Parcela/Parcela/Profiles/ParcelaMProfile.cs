using AutoMapper;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za parcele
    /// </summary>
    public class ParcelaMProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za parcele
        /// </summary>
        public ParcelaMProfile()
        {
            CreateMap<ParcelaM, ParcelaDto>();
        }
    }
}
