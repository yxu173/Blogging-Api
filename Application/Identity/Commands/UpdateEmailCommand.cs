using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public record UpdateEmailCommand(Guid Id, string EmailAddress)
    : IRequest<OperationResult<EmailUpdateDto>>;