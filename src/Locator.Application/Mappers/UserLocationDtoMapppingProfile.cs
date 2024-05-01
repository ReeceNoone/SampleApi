using AutoMapper;
using Locator.Contracts.Responses;
using Locator.Domain.Models;

namespace Locator.Application.Mappers;

public class UserLocationDtoMapppingProfile : Profile
{
    public UserLocationDtoMapppingProfile()
    {
        CreateMap<Location, UserLocationDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dst => dst.LeftAt, opt => opt.MapFrom(src => src.LeftAt));
    }
}