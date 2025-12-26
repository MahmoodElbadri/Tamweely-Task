
using AutoMapper;
using Tamweely.Application.DTOs;
using Tamweely.Domain.Entities;

namespace Tamweely.Application.Profiles;

public class DepartmentProfile:Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<CreateDepartmentDto, Department>();
    }
}
