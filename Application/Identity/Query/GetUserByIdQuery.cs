using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Query;

public record GetUserByIdQuery(Guid Id) : IRequest<OperationResult<IdentityUserDto>>;