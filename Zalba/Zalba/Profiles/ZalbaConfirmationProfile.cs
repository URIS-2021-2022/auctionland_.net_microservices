using AutoMapper;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Profiles
{
    /// <summary>
    /// Profil za potvrdu zalbi
    /// </summary>
    public class ZalbaConfirmationProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za potvrdu zalbi
        /// </summary>
        public ZalbaConfirmationProfile()
        {
            CreateMap<ZalbaConfirmation, ZalbaConfirmationDto>();
        }
    }
}
