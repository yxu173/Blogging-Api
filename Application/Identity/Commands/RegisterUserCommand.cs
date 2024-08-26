using Application.Identity.DTOs;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Commands;

public class RegisterUserCommand : IRequest<OperationResult<IdentityUserDto>>
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}