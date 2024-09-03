using Application.Identity.DTOs;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Query;

public record GetUserByUserNameQuery(string UserName) : IRequest<OperationResult<UserProfileDto>>;