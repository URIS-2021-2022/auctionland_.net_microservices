using AutoMapper;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za potvrdu delova parcele
    /// </summary>
    public class DeoParceleConfirmationProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za potvrdu delova parcele
        /// </summary>
        public DeoParceleConfirmationProfile()
        {
            CreateMap<DeoParceleConfirmation, DeoParceleConfirmationDto>();
        }
    }
}
