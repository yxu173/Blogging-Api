using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Query;

public class GetAllPostsByTagNameQuery : IRequest<OperationResult<List<PostDto>>>
{
    public required string TagName { get; set; }
}