using AutoMapper;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Profiles
{
    /// <summary>
    /// Profil za tipove zalbi
    /// </summary>
    public class TipZalbeProfile : Profile
    {
        /// <summary>
        /// Konstruktor rofila za tipove zalbi
        /// </summary>
        public TipZalbeProfile()
        {
            CreateMap<TipZalbe, TipZalbeDto>();
        }
    }
}
