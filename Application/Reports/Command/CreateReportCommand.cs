using Application.Models;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Reports.Command;

public record CreateReportCommand(Guid ContentId, ContentType ContentType, Reason Reason) 
    : IRequest<OperationResult<Report>>;