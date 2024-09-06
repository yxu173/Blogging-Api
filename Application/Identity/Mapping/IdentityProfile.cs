using Application.Identity.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Identity.Mapping;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<User, IdentityUserDto>()
            .ForMember(des => des.UserName,
                opt =>
                    opt.MapFrom(src => src.UserName))
            .ForMember(des => des.Email,
                opt =>
                    opt.MapFrom(src => src.Email));
    }
}