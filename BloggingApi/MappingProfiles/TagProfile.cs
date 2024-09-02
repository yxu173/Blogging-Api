using Application.PostTags.DTOs;
using AutoMapper;
using BloggingApi.Contracts.Tag.Response;
using Domain.Entities;

namespace BloggingApi.MappingProfiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
        CreateMap<TagDto, TagResponse>();
    }
}