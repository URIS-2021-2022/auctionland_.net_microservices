using AutoMapper;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za potvrdu parcela
    /// </summary>
    public class ParcelaConfirmationProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za potvrdu parcela
        /// </summary>
        public ParcelaConfirmationProfile()
        {
            CreateMap<ParcelaConfirmation, ParcelaConfirmationDto>();
        }
    }
}
