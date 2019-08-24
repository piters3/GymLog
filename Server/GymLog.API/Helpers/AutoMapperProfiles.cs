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
            CreateMap<UserRole, RoleModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<User, UserDetailsModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles));
            CreateMap<Role, RoleModel>();
            CreateMap<User, UserSummary>();
            CreateMap<UserDetailsModel, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());
            CreateMap<RoleModel, Role>();
        }
    }
}