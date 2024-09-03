using Application.Identity.DTOs;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Commands;

public record RegisterUserCommand(string UserName, string Email, string Password)
    : IRequest<OperationResult<IdentityUserDto>>;