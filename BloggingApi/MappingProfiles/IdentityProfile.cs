using Application.Identity.Commands;
using Application.Identity.DTOs;
using AutoMapper;
using BloggingApi.Contracts.Identity;
using Domain.Entities;

namespace BloggingApi.MappingProfiles;

public sealed class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<RegisterCreate, RegisterUserCommand>();
        CreateMap<IdentityUserDto, IdentityResponse>();
        CreateMap<User, IdentityUserDto>();
        CreateMap<LoginCreate, LoginUserCommand>();
        CreateMap<User, IdentityResponse>();
        CreateMap<User, UsernameUpdateDto>();
        CreateMap<User, EmailUpdateDto>();
        CreateMap<UsernameUpdateDto, UserUpdate>();
        
    }
}