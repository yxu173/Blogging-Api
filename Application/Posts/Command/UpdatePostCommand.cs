using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public class UpdatePostCommand : IRequest<OperationResult<bool>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}