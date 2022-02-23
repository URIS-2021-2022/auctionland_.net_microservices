using AutoMapper;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za potvrdu opstina
    /// </summary>
    public class OpstinaConfirmationProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za potvrdu opstina
        /// </summary>
        public OpstinaConfirmationProfile()
        {
            CreateMap<OpstinaConfirmation, OpstinaConfirmationDto>();
        }
    }
}
