using Application.Roles.DTOs;
using AutoMapper;
using BloggingApi.Contracts.Role.Response;
using Domain.Entities;

namespace BloggingApi.MappingProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, RoleResponse>();
    }
}