using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public record VerificationEmailCommand(Guid Token) : IRequest<OperationResult<bool>>;