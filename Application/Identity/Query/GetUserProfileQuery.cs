using Application.Identity.DTOs;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Query;

public class GetUserProfileQuery : IRequest<OperationResult<UserProfileDto>>
{
    public Guid UserId { get; set; }
}