using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public record RemoveTagFromPostCommand(Guid PostId, Guid TagId) : IRequest<OperationResult<bool>>;