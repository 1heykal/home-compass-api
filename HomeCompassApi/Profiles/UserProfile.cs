using AutoMapper;
using HomeCompassApi.Models;
using HomeCompassApi.Services.User;

namespace HomeCompassApi.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDetailsDTO>();
        CreateMap<UpdateUserDetailsDTO, ApplicationUser>();
    }
}