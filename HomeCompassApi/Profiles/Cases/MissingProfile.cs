using AutoMapper;
using HomeCompassApi.Entities.Cases;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Cases.Missing;

namespace HomeCompassApi.Profiles.Cases;

public class MissingProfile : Profile
{
    public MissingProfile()
    {
        CreateMap<Missing, MissingDTO>();
    }  
}