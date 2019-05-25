using AutoMapper;
using GymLog.API.Entities;
using GymLog.API.Models;

namespace GymLog.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<User, UserDetailsModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles));
        }
    }
}