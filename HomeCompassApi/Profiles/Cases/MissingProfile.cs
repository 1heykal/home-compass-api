using AutoMapper;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Services.Cases;

namespace HomeCompassApi.Profiles.Cases;

public class MissingProfile : Profile
{
    public MissingProfile()
    {
        CreateMap<Missing, MissingDTO>();
    }  
}