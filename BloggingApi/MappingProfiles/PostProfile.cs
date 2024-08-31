using Application.Posts.DTOs;
using AutoMapper;
using BloggingApi.Contracts.Post;
using BloggingApi.Contracts.Post.Response;
using Domain.Entities;

namespace BloggingApi.MappingProfiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<PostDto, PostResponse>();
        CreateMap<LikeDto, LikeResponse>();
        CreateMap<CommentDto, CommentResponse>();
        CreateMap<Post, PostDto>();
        CreateMap<Post, PostResponse>();
        CreateMap<Like, LikeDto>();
        CreateMap<Comment, CommentDto>();
    }
}