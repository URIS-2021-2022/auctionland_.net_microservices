using AutoMapper;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Profiles
{
    /// <summary>
    /// Profil za potvrdu tipova zalbi
    /// </summary>
    public class TipZalbeConfirmationProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za potvrdu tipova zalbi
        /// </summary>
        public TipZalbeConfirmationProfile()
        {
            CreateMap<TipZalbeConfirmation, TipZalbeConfirmationDto>();
        }
    }
}
