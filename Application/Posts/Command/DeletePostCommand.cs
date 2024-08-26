using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public class DeletePostCommand : IRequest<OperationResult<bool>>
{
    public Guid Id { get; set; }
}