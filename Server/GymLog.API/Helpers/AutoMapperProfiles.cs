using AutoMapper;
using GymLog.API.Entities;
using GymLog.API.Models;

namespace GymLog.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>()
                .ConstructUsing(x => new User());
            CreateMap<UserRole, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<User, UserDetailsDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles));
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserSummaryDto>();
            CreateMap<UserDetailsDto, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());
            CreateMap<RoleDto, Role>();
        }
    }
}