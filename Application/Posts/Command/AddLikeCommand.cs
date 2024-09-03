using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Posts.Command;

public record AddLikeCommand(Guid UserId, Guid PostId, InteractionType InteractionType) 
     : IRequest<OperationResult<LikeDto>>;