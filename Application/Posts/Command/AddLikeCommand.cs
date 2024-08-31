using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Posts.Command;

public class AddLikeCommand : IRequest<OperationResult<LikeDto>>
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public required InteractionType InteractionType { get; set; }
}