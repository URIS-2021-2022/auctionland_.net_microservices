using AutoMapper;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Profiles
{
    /// <summary>
    /// Profil za zalbe
    /// </summary>
    public class ZalbaMProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za zalbe
        /// </summary>
        public ZalbaMProfile()
        {
            CreateMap<ZalbaM, ZalbaDto>();
        }
    }
}
