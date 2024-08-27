using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public class UpdateEmailCommand : IRequest<OperationResult<EmailUpdateDto>>
{
    public required Guid Id { get; set; }
    public required string EmailAddress { get; set; }
}