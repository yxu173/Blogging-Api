using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public record LoginUserCommand(string UserName, string Password) : IRequest<OperationResult<IdentityUserDto>>;