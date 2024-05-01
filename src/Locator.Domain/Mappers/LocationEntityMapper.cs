using AutoMapper;
using Locator.Domain.Models;
using Locator.Persistence.Entities;

namespace Locator.Domain.Mappers;

public class LocationEntityMapper : Profile
{
    public LocationEntityMapper()
    {
        CreateMap<LocationEntity, Location>()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(m => m.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(m => m.Longitude))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(m => m.CreatedAt))
            .ForMember(dest => dest.LeftAt, opt => opt.MapFrom(m => m.LeftAt))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(m => m.Address))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(m => m.UserId));
    }
}