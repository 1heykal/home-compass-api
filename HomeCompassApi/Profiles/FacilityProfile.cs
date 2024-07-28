using AutoMapper;
using HomeCompassApi.Entities.Facilities;
using HomeCompassApi.Models.Facilities;

namespace HomeCompassApi.Profiles;

public class FacilityProfile : Profile
{
    public FacilityProfile()
    {
        CreateMap<Facility, ReadFacilitiesDTO>();
    }
}