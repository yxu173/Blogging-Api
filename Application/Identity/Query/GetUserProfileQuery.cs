using Application.Identity.DTOs;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Query;

public record GetUserProfileQuery(Guid UserId) : IRequest<OperationResult<UserProfileDto>>;