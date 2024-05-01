using AutoMapper;
using Locator.Domain.Models;
using Locator.Persistence.Entities;

namespace Locator.Domain.Mappers;

public class UserEntityMapper : Profile
{
    public UserEntityMapper()
    {
        CreateMap<User, UserEntity>()
            .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
            .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
            .ForMember(x => x.FirstName, x => x.MapFrom(m => m.FirstName))
            .ForMember(x => x.LastName, x => x.MapFrom(m => m.LastName));

        CreateMap<UserEntity, User>()
            .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
            .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
            .ForMember(x => x.FirstName, x => x.MapFrom(m => m.FirstName))
            .ForMember(x => x.LastName, x => x.MapFrom(m => m.LastName));
    }
}