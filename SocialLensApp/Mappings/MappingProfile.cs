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
            CreateMap<Invite, InviteDto>();
            CreateMap<User, UserDto>();
            CreateMap<CreatePostDto,Post >();
            CreateMap<Post, PostDto>();
            CreateMap<Post, EditPostDto>();
            CreateMap<CommentDto, Comment>();
        }
    }
}
