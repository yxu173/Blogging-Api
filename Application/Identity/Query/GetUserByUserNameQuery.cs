using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Query;

public class GetUserByUserNameQuery : IRequest<OperationResult<User>>
{
    public required string UserName { get; set; }
}