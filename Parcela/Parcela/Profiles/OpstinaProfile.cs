using AutoMapper;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za opstine
    /// </summary>
    public class OpstinaProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za opstine
        /// </summary>
        public OpstinaProfile()
        {
            CreateMap<Opstina, OpstinaDto>();
        }
    }
}
