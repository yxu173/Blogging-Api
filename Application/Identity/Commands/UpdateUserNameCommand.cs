using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public record UpdateUserNameCommand(Guid Id, string UserName) 
    : IRequest<OperationResult<UsernameUpdateDto>>;