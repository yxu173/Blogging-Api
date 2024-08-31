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
            .ForMember(des => des.EmailAddress,
                opt =>
                    opt.MapFrom(src => src.Email));

        CreateMap<User, UsernameUpdateDto>()
            .ForMember(des => des.UserName,
                opt =>
                    opt.MapFrom(src => src.UserName));
        CreateMap<User, EmailUpdateDto>()
            .ForMember(des => des.EmailAddress,
                opt =>
                    opt.MapFrom(src => src.Email));

    }
}