using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Query;

public record GetPostByIdQuery(Guid Id) : IRequest<OperationResult<PostDto>>;