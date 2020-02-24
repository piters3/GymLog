using AutoMapper;
using GymLog.API.DTOs;
using GymLog.API.Entities;

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
            CreateMap<Muscle, MuscleDto>();
            CreateMap<MuscleDto, Muscle>();
            CreateMap<Workout, WorkoutDto>()
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Exercise.Id))
                .ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise.Name));
            CreateMap<WorkoutDto, Workout>();
            CreateMap<Set, SetDto>();
            CreateMap<SetDto, Set>();
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, Exercise>();
            //CreateMap<Daylog, DaylogDto>().ForMember(dest => dest.Workouts, opt => opt.MapFrom(src => src.WorkoutDaylogs.Select(x => x.Workout)));
            //CreateMap<Daylog, DaylogDto>().ForMember(dest => dest.Workouts, opt => opt.MapFrom(src => src.Workouts));
            CreateMap<Daylog, DaylogDto>();
            CreateMap<DaylogDto, Daylog>();
        }
    }
}