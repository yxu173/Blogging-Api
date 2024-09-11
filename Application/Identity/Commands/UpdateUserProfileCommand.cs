using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Commands;

public record UpdateUserProfileCommand(Guid Id, string Bio, string SocialMediaLinks)
    : IRequest<OperationResult<BasicInfo>>;