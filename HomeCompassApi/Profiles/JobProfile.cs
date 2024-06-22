using AutoMapper;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services.Facilities;

namespace HomeCompassApi.Profiles;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, ReadJobsDTO>();
    }
}