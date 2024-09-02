using Application.Follows.DTOs;
using AutoMapper;
using BloggingApi.Contracts.Follow;
using Domain.Entities;

namespace BloggingApi.MappingProfiles;

public class FollowProfile : Profile
{
    public FollowProfile()
    {
        CreateMap<Follow, UserFollowDto>();
        CreateMap<UserFollowDto, FollowResponse>();
    }
}