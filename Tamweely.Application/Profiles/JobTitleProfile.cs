using AutoMapper;
using Tamweely.Application.DTOs;
using Tamweely.Domain.Entities;

namespace Tamweely.Application.Profiles;

public class JobTitleProfile:Profile
{
    public JobTitleProfile()
    {
        CreateMap<JobTitle, JobTitleDto>();
        CreateMap<CreateJobTitle, JobTitle>();
    }
}
