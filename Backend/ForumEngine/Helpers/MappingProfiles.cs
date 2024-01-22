using AutoMapper;
using ForumEngine.Dtos;
using ForumEngine.Models;

namespace ForumEngine.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}