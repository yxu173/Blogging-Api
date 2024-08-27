using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Identity.Commands;

public class UpdateUserProfileCommand : IRequest<OperationResult<BasicInfo>>
{
    public required Guid Id { get; set; }
    public required string Bio { get; set; }
    public required string ProfileImage { get; set; }
    public required string SocialMediaLinks { get; set; }
}