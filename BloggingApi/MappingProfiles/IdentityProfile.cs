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
        CreateMap<IdentityUserDto, IdentityResponse>();
        CreateMap<User, IdentityUserDto>();
        CreateMap<User, IdentityResponse>();
        CreateMap<UserProfileDto, UserProfileResponse>();
    }
}