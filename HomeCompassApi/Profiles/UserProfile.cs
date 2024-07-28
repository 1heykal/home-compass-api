using AutoMapper;
using HomeCompassApi.Entities;
using HomeCompassApi.Models.User;

namespace HomeCompassApi.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDetailsDTO>();
        CreateMap<UpdateUserDetailsDTO, ApplicationUser>();
    }
}