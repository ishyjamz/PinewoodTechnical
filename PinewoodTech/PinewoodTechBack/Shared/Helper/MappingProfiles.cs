using AutoMapper;
using PinewoodTechBack.Shared.Dtos;
using PinewoodTechBack.Shared.Models;


namespace PinewoodTechBack.Shared.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
    }
}