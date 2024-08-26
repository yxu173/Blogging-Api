using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Query;

public class GetPostByIdQuery : IRequest<OperationResult<Post>>
{
    public Guid Id { get; set; }
}