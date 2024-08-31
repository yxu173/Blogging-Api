using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Query;

public class GetPostByIdQuery : IRequest<OperationResult<PostDto>>
{
    public Guid Id { get; set; }
}