using AutoMapper;
using HomeCompassApi.Entities.Facilities;
using HomeCompassApi.Models.Facilities;

namespace HomeCompassApi.Profiles;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, ReadJobsDTO>();
    }
}