using AutoMapper;
using HomeCompassApi.Entities.Cases;
using HomeCompassApi.Models.Cases.Homeless;

namespace HomeCompassApi.Profiles.Cases;

public class HomelessProfile : Profile
{
    public HomelessProfile()
    {
        CreateMap<Homeless, HomelessDTO>();
    }
}