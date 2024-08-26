using AutoMapper;
using BloggingApi.Contracts.Post;
using Domain.Entities;

namespace BloggingApi.MappingProfiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostResponse>();
    }
}