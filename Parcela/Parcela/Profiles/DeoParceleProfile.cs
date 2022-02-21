using AutoMapper;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za delove parcele
    /// </summary>
    public class DeoParceleProfile : Profile
    {
        /// <summary>
        /// Konstruktor rofila za delove parcele
        /// </summary>
        public DeoParceleProfile()
        {
            CreateMap<DeoParcele, DeoParceleDto>();
        }
    }
}
