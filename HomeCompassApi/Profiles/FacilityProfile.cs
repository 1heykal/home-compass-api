using AutoMapper;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services.Facilities;

namespace HomeCompassApi.Profiles;

public class FacilityProfile : Profile
{
    public FacilityProfile()
    {
        CreateMap<Facility, ReadFacilitiesDTO>();
    }
}