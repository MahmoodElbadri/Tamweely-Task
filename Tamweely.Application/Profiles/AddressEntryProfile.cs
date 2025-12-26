using AutoMapper;
using Tamweely.Application.DTOs;
using Tamweely.Domain.Entities;

namespace Tamweely.Application.Profiles;

public class AddressEntryProfile : Profile
{
    public AddressEntryProfile()
    {
        CreateMap<AddressEntry, AddressEntryDto>();
        CreateMap<CreateAddressEntryDto, AddressEntry>()
            .ForMember(dest => dest.PhotoPath, opt => opt.Ignore());
    }
}
