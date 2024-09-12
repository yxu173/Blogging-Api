using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Command;

public record CreatePostCommand(Guid UserId, string Title, string Content, List<IFormFile> Images)
    : IRequest<OperationResult<Post>>;