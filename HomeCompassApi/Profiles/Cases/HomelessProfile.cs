using AutoMapper;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Services.Cases.Homeless;

namespace HomeCompassApi.Profiles.Cases;

public class HomelessProfile : Profile
{
    public HomelessProfile()
    {
        CreateMap<Homeless, HomelessDTO>();
    }
}