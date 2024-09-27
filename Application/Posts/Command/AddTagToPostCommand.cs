using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public record AddTagToPostCommand(Guid PostId, Guid TagId) : IRequest<OperationResult<bool>>;