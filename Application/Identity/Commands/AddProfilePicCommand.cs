using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Identity.Commands;

public record AddProfilePicCommand(Guid UserId, IFormFile File) : IRequest<OperationResult<string>>;