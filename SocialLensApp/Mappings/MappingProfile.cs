using AutoMapper;
using SocialLensApp.Entities;
using SocialLensApp.Models;

namespace SocialLensApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDto, User>();
        }
    }
}
