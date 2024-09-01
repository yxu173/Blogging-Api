using Application.Identity.DTOs;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Query;

public class GetUserByUserNameQuery : IRequest<OperationResult<UserProfileDto>>
{
    public required string UserName { get; set; }
}