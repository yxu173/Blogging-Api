using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public record UpdatePostCommand(Guid Id, Guid UserId, string Title, string Content) 
    : IRequest<OperationResult<bool>>;